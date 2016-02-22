using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignBoards.Models
{
    public class SignBoardCreateViewModel
    {
        public Guid CompanyId { get; set; }

        public SignBoard SignBoard { get; set; }

        public SignBoardAddress SignBoardAddress { get; set; }

        public Guid CompanyAddressId { get; set; }

        public Guid FittingType { get; set; }

        public IEnumerable<SelectListItem> FittingTypes { get; set; }
    }
}