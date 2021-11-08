// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResponseModel.cs" company="TVSNEXT">
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
    /// Response model class
    /// </summary>
    /// <typeparam name="T"> Generic type</typeparam>
    public class ResponseModel<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether status as true or false
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message as string
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data as generic type
        /// </summary>
        public T Data { get; set; }
    }
}
