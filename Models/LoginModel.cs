// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// login model data class
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the email id
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }
    }
}
