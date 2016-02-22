using System;

namespace SignBoards.Models
{
    public class SignBoardBid : EntityBase
    {
        public Guid SignBoardId { get; set; }

        public SignBoard SignBoard { get; set; }

        public Guid ContractorId { get; set; }

        public Contractor Contractor { get; set; }

        public decimal BidAmount { get; set; }

        public DateTime BidDate { get; set; }

        public bool IsBidWinner { get; set; }
    }
}