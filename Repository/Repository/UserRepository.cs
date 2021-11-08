// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.Configuration;
    using Models;
    using global::Repository.Context;
    using global::Repository.Interface;
     
    /// <summary>
    /// User repository class
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Declaring UserContext 
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class
        /// </summary>
        /// <param name="userContext">passing a user context</param>
        /// <param name="configuration">passing a configuration</param>
        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets  the configuration
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// new Registration for user
        /// </summary>
        /// <param name="userData">RegisterModel Data</param>
        /// <returns>returns string message</returns>
        public string Register(RegisterModel userData)
        {
            try
            {
                var check = this.userContext.UsersData.Where(x => x.EmailId == userData.EmailId).FirstOrDefault();
                if (check == null)
                {
                    if (userData != null)
                    {
                        userData.Password = this.EncryptPassWord(userData.Password);
                        this.userContext.UsersData.Add(userData);
                        this.userContext.SaveChanges();
                        return "Registration Successful";
                    }
                }

                return "User Already Exist OR Registration UnSuccessful";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="loginData">passing a login model</param>
        /// <returns>returns a register model</returns>
        public RegisterModel Login(LoginModel loginData)
        {
            try
            {
                string encodedPassword = this.EncryptPassWord(loginData.Password);
                var login = this.userContext.UsersData.Where(x => x.EmailId == loginData.EmailId && x.Password == encodedPassword).FirstOrDefault();

                if (login == null)
                {
                    return null;
                }
                else
                {
                    return login;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        /// <summary>
        /// Encrypt The Password
        /// </summary>
        /// <param name="password">Passing Password To Encrypt</param>
        /// <returns>Encrypted Password</returns>
        private string EncryptPassWord(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(passwordInBytes);
            return encodePassword;
        }
    }
}
