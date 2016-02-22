using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignBoards.Models
{
    public class Contractor : EntityBase
    {
        public Guid ContactId { get; set; }

        public Contact Contact { get; set; }

        public Guid ContractorAddressId { get; set; }
        public ContractorAddress ContractorAddress { get; set; }
    }
}