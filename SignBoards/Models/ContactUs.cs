using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignBoards.Models
{
    public class ContactUs : EntityBase
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }
    }
}