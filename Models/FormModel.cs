// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormModel.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// form model data class
    /// </summary>
    public class FormModel
    {
        /// <summary>
        /// Gets or sets the form id(primary key) 
        /// </summary>
        [Key]
        public int FormId { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the reason for loan
        /// </summary>
        public string ReasonForLoan { get; set; }

        /// <summary>
        /// Gets or sets the loan amount 
        /// </summary>
        public double loanAmount { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user id as foreign key
        /// </summary>
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
