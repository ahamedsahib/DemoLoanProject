using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class FormModel
    {
        [Key]
        public int FormId { get; set; }


        public string Status { get; set; }

        public string ReasonForLoan { get; set; }

        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }

        public virtual RegisterModel RegisterModel { get; set; }
    }
}
