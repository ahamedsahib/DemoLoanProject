using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Repository.Repository
{
    public class PropertyRepository: IPropertyRepository
    {
       
        private readonly UserContext userContext;

        private readonly IConfiguration configuration;
        public PropertyRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        public FormModel AddForm(FormList formList)
        {
            try
            {
                FormModel form = new FormModel();
                form.ReasonForLoan=formList.ReasonForLoan;
                form.UserId = formList.UserId;
                form.Status = "Pending";
                if (form != null)
                {
                    this.userContext.Forms.Add(form);
                    this.userContext.SaveChanges();
                    var formData = this.userContext.Forms.Where(a => a.ReasonForLoan.Equals(formList.ReasonForLoan) && a.UserId==formList.UserId && a.Status.Equals("Pending")).FirstOrDefault();
                    AddProperty(formList.propertiesList,formData.FormId,formData.UserId);
                    return formData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string AddProperty(List<PropertyModel> propertyData,int formId,int userId)
        {
            try
            {
                if (propertyData!=null)
                {
                  foreach(var data in propertyData)
                    {
                        data.FormId = formId;
                        data.UserId = userId;
                    }
                    this.userContext.Property.AddRange(propertyData);
                    this.userContext.SaveChanges();
                    CalculateLoan(formId);
                }
                return "success";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string CalculateLoan(int formId)
        {
            string message = "";
            var userId = this.userContext.Forms.Where(x => x.FormId == formId).Select(x=>x.UserId).FirstOrDefault();
            var email = this.userContext.UsersData.Where(x => x.UserId == userId).Select(x => x.EmailId).FirstOrDefault();
            try
            {
                var f = this.userContext.Forms.Where(a => a.FormId == formId).FirstOrDefault();
                var d = this.userContext.Property.Where(a=>a.FormId == formId).ToList();
                var sumOfProperty = d.Select(x => x.PropertyWorth).Sum();
                var Amount = 0.1 * sumOfProperty;

                if (Amount > 0)
                {
                    f.Status = "Approved";
                    f.loanAmount = Amount;
                    this.userContext.SaveChanges();
                    message = f.Status;
                    SendMSMQ(f, Amount);

                    if (this.SendMail(email))
                    {
                        return "Your loan is approved for amount " + " " + Amount + " " + f.ReasonForLoan;
                
                    }
                    else
                    {
                        return "Mail not sent";
                    }
                }
                else
                {
                    f.Status = "Denied";
                    this.userContext.SaveChanges();
                    message = f.Status;
                    SendMSMQ(f, Amount);

                    if (this.SendMail(email))
                    {
                        return "Your loan is denied" + " " + Amount + " " + f.ReasonForLoan;
                    }
                    else
                    {
                        return "Mail not sent";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SendMSMQ(FormModel formModel,double loanAmount)
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
            message.Body = formModel.Status + loanAmount;
            msgqueue.Send(message);
        }


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

        private string ReceiveMSMQ(string email)
        {
            var receivequeue = new MessageQueue(@".\Private$\MyQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();
            string emailMessage = receivemsg.Body.ToString();
            return emailMessage;
        }

    
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


        public List<PropertyModel> GetPropertyDetails(int userId)
        {
            try
            {
                var propertyData = this.userContext.Property.Where(a => a.UserId == userId).ToList();
                if (propertyData.Count>0)
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
    }
}
