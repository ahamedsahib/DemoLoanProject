using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class RegisterModel
    {
        [Key]
        public int UserId { get; set; }

      
        public string UserName { get; set; }
    
        public string Gender { get; set; }
 
        public string PhoneNumber { get; set; }
     
        public string EmailId { get; set; }
      
        public string Password { get; set; }

        public string Profession { get; set; }

    }
}
