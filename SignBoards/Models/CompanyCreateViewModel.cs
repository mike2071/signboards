using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignBoards.Models
{
    public class CompanyCreateViewModel
    {
        public Company Company { get; set; }

        public Contact Contact { get; set; }

        public CompanyAddress CompanyAddress { get; set; }
    }
}