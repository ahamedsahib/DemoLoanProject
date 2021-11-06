// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;

    /// <summary>
    /// interface for user manager class
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// definition for register method
        /// </summary>
        /// <param name="userData">passing a register model</param>
        /// <returns>returns as string value</returns>
        string Register(RegisterModel userData);

        /// <summary>
        /// Definition for login method
        /// </summary>
        /// <param name="loginData">passing a login model</param>
        /// <returns>returns a register model</returns>
        RegisterModel Login(LoginModel loginData);

        /// <summary>
        /// Definition for Get user details
        /// </summary>
        /// <param name="userId">passing a user id</param>
        /// <returns>returns a register model</returns>
        RegisterModel GetUserDetails(int userId);
    }
}
