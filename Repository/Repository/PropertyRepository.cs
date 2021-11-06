// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyRepository.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Experimental.System.Messaging;
    using Microsoft.Extensions.Configuration;
    using Models;
    using global::Repository.Context;
    using global::Repository.Interface;

    /// <summary>
    /// Property repository class
    /// </summary>
    public class PropertyRepository : IPropertyRepository
    {
        /// <summary>
        /// Declaring UserContext 
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Gets the configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyRepository"/> class
        /// </summary>
        /// <param name="userContext">passing a user context</param>
        /// <param name="configuration">passing a configuration</param>
        public PropertyRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Add Form method
        /// </summary>
        /// <param name="formList">passing a form list data</param>
        /// <returns>returns a string message</returns>
        public string AddForm(FormList formList)
        {
            try
            {
                FormModel form = new FormModel();
                form.ReasonForLoan = formList.ReasonForLoan;
                form.UserId = formList.UserId;
                form.Status = "Pending";
                if (form != null)
                {
                    this.userContext.Forms.Add(form);
                    this.userContext.SaveChanges();
                    var formData = this.userContext.Forms.Where(a => a.ReasonForLoan.Equals(formList.ReasonForLoan) && a.UserId == formList.UserId && a.Status.Equals("Pending")).FirstOrDefault();
                    this.AddProperty(formList.propertiesList, formData.FormId, formData.UserId);
                    return "Form Added Successfully";
                }

                return "Form Not Added Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get property details method
        /// </summary>
        /// <param name="userId">passing a user id as integer</param>
        /// <returns>returns a list of form model</returns>
        public List<FormModel> GetPropertyDetails(int userId)
        {
            try
            {
                var propertyData = this.userContext.Forms.Where(a => a.UserId == userId).ToList();
                if (propertyData.Count > 0)
                {
                    return propertyData;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Property method
        /// </summary>
        /// <param name="propertyData">passing a list of property data</param>
        /// <param name="formId">passing a form id as integer</param>
        /// <param name="userId">passing a user id as integer</param>
        private void AddProperty(List<PropertyModel> propertyData, int formId, int userId)
        {
            try
            {
                if (propertyData != null)
                {
                  foreach (var data in propertyData)
                    {
                        data.FormId = formId;
                        data.UserId = userId;
                    }

                    this.userContext.Property.AddRange(propertyData);
                    this.userContext.SaveChanges();
                    this.CalculateLoan(formId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      /// <summary>
      /// Calculate loan 
      /// </summary>
      /// <param name="formId">passing a form id as integer</param>
      /// <returns>returns a true or false message</returns>
        private bool CalculateLoan(int formId)
        {
            string message = string.Empty;
            var userId = this.userContext.Forms.Where(x => x.FormId == formId).Select(x => x.UserId).FirstOrDefault();
            var email = this.userContext.UsersData.Where(x => x.UserId == userId).Select(x => x.EmailId).FirstOrDefault();
            try
            {
                var f = this.userContext.Forms.Where(a => a.FormId == formId).FirstOrDefault();
                var d = this.userContext.Property.Where(a => a.FormId == formId).ToList();
                var sumOfProperty = d.Select(x => x.PropertyWorth).Sum();
                var amount = 0.1 * sumOfProperty;

                if (amount > 0)
                {
                    f.Status = "Approved";
                    f.loanAmount = amount;
                    this.userContext.SaveChanges();
                    message = f.Status;
                    this.SendMSMQ(f, amount);

                    if (this.SendMail(email))
                    {
                        return true;
                    }
                    else
                    {
                        return false; 
                    }
                }
                else
                {
                    f.Status = "Denied";
                    this.userContext.SaveChanges();
                    message = f.Status;
                    this.SendMSMQ(f, amount);

                    if (this.SendMail(email))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Send message queue method to send message to queue
        /// </summary>
        /// <param name="formModel">passing a form model data</param>
        /// <param name="loanAmount">passing a loan amount(double)</param>
        private void SendMSMQ(FormModel formModel, double loanAmount)
        {
            MessageQueue msgqueue;

            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msgqueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
            }

            Message message = new Message();
            var formatter = new BinaryMessageFormatter();
            message.Formatter = formatter;
            msgqueue.Label = "url Link";
            if (formModel.Status.Equals("Approved"))
            {
                message.Body = "Your loan is approved for amount " + " " + loanAmount + " " + formModel.ReasonForLoan;
            }
            else
            {
                message.Body = "Your loan was denied";
            }

            msgqueue.Send(message);
        }

        /// <summary>
        /// send mail to authorized person
        /// </summary>
        /// <param name="email">passing a string email id </param>
        /// <returns>returns true or false</returns>
        private bool SendMail(string email)
        {
            string emailMessage = this.ReceiveMSMQ(email);
            if (this.SendMailToUser(email, emailMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Receive message queue
        /// </summary>
        /// <param name="email">passing a string email id </param>
        /// <returns>returns a string email message </returns>
        private string ReceiveMSMQ(string email)
        {
            var receivequeue = new MessageQueue(@".\Private$\MyQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();
            string emailMessage = receivemsg.Body.ToString();
            return emailMessage;
        }

        /// <summary>
        /// SMTP Configuration
        /// </summary>
        /// <param name="email">Email Id</param>
        /// <param name="message">Message or url</param>
        /// <returns>returns true</returns>
        private bool SendMailToUser(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            mailMessage.From = new MailAddress("radhika.shankar1220@gmail.com");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Loan Approval Message";
            mailMessage.Body = message;
            smtp.EnableSsl = true;
            mailMessage.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("radhika.shankar1220@gmail.com", "kriyanthi");
            smtp.Send(mailMessage);
            return true;
        }
    }
}
