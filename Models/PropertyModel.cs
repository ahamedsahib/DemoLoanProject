// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyModel.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// property model data class
    /// </summary>
    public class PropertyModel
    {
        /// <summary>
        /// Gets or sets the property id(primary key) 
        /// </summary>
        [Key]
        public int PropertyId { get; set; }

        /// <summary>
        /// Gets or sets the property
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the property worth
        /// </summary>
        public long PropertyWorth { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        [ForeignKey("RegisterModel")]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user id as foreign key
        /// </summary>
        public virtual RegisterModel RegisterModel { get; set; }

        /// <summary>
        /// Gets or sets the form id
        /// </summary>
        [ForeignKey("FormModel")]
        public int FormId { get; set; }

        /// <summary>
        /// Gets or sets the form id as foreign key
        /// </summary>
        public virtual FormModel FormModel { get; set; }
    }
}