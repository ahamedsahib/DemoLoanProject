using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FormList
    {
        public string ReasonForLoan { get; set; }
        public List<PropertyModel> propertiesList { get; set; }

        public int UserId { get; set; }
    }
}
