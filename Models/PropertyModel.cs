using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class PropertyModel
    {
        [Key]
        public int PropertyId { get; set; }

        public string Property { get; set; }

        public string PropertyWorth { get; set; }

        public string Status { get; set; }

        [Display(Name = "RegisterModel")]
        public virtual int UserId { get; set; }

    
        [ForeignKey("UserId")]
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
