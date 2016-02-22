using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignBoards.Models
{
    public class SignBoardsModel
    {
        public Guid Id { get; set; }

        public Guid CreatedByUserId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyFirstlineAddress { get; set; }

        public string FittingAddressPostcode { get; set; }

        public string FittingInstructions { get; set; }

        public decimal FittingCharge { get; set; }

        public string FittingType { get; set; }
    }
}