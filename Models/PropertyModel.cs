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

        public long PropertyWorth { get; set; }


        [ForeignKey("RegisterModel")]
        public  int? UserId { get; set; }

        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("FormModel")]
        public  int FormId { get; set; }
        public virtual FormModel FormModel { get; set; }

    }
}