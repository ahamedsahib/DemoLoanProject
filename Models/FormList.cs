// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormList.cs" company="TVSNEXT">
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
    /// Form list data class
    /// </summary>
    public class FormList
    {
        /// <summary>
        /// Gets or sets the reason for loan
        /// </summary>
        public string ReasonForLoan { get; set; }

        /// <summary>
        /// Gets or sets the property list 
        /// </summary>
        public List<PropertyModel> propertiesList { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public int UserId { get; set; }
    }
}
