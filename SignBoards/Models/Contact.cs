using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SignBoards.Models
{
    public class Contact: EntityBase
    {
        [DisplayName("Contact Name")]
        public string Name { get; set; }

        [DisplayName("Telephone Number")]
        public int TelephoneNumber { get; set; }

        [DisplayName("Mobile Number")]
        public int MobileNumber { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
    }
}