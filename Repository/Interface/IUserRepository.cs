// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;

    /// <summary>
    /// interface for user repository class
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Register Method definition
        /// </summary>
        /// <param name="userData">passing a register model</param>
        /// <returns>returns a string message</returns>
        string Register(RegisterModel userData);
        
        /// <summary>
        /// Login Definition Method
        /// </summary>
        /// <param name="loginData">passing a login model</param>
        /// <returns>returns a register model</returns>
        RegisterModel Login(LoginModel loginData);
    }
}
