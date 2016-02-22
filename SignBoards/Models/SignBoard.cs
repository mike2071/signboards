using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SignBoards.Models
{
    public class SignBoard : EntityBase
    {
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }
        
        public Guid SignBoardAddressId { get; set; }

        public SignBoardAddress SignBoardAddress { get; set; }

        public string FittingInstructions { get; set; }

        [DataType(DataType.Currency)]
        public decimal FittingCharge { get; set; }

        public Guid FittingTypeId { get; set; }
    }
}