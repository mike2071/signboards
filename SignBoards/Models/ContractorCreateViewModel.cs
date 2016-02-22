using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignBoards.Models
{
    public class ContractorCreateViewModel
    {
        public Contractor Contractor { get; set; }

        public Contact Contact { get; set; }

        public ContractorAddress ContractorAddress { get; set; }
    }
}