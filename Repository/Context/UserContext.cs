// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserContext.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Context
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Models;

    /// <summary>
    /// UserContext Class
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class
        /// </summary>
        /// <param name="options">options as parameter</param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets Database set property for users data  table
        /// </summary>
        public DbSet<RegisterModel> UsersData { get; set; }

        /// <summary>
        /// Gets or sets Database set property for property table
        /// </summary>
        public DbSet<PropertyModel> Property { get; set; }

        /// <summary>
        /// Gets or sets Database set property for forms table
        /// </summary>
        public DbSet<FormModel> Forms { get; set; }
    }
}
