using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SignBoards.Models
{
    public class SignBoardBidCreateViewModel
    {
        public Guid SignBoardBidId { get; set; }

        public Guid ContractorIdId { get; set; }

        public SignBoardBid SignBoardBid { get; set; }
    }
}