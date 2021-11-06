// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using global::Manager.Interface;
    using Models;
    using Repository.Interface;

    /// <summary>
    /// Class User manager
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// declaring repository
        /// </summary>
        private readonly IUserRepository userrepoistory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="userrepoistory">user repository as parameter</param>
        public UserManager(IUserRepository userrepoistory)
        {
            this.userrepoistory = userrepoistory;
        }

       /// <summary>
       /// Register Method
       /// </summary>
       /// <param name="userData">passing a register model</param>
       /// <returns>returns a string message</returns>
        public string Register(RegisterModel userData)
        {
            try
            {
                return this.userrepoistory.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Login Method
        /// </summary>
        /// <param name="loginData">passing a login model data</param>
        /// <returns>Returns a register model</returns>
        public RegisterModel Login(LoginModel loginData) 
        {
            try
            {
                return this.userrepoistory.Login(loginData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
