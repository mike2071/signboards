using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignBoards.Models
{
    public class Company : EntityBase
    {
        [DisplayName("Company Name")]
        public string Name { get; set; }

        [Required]
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
        
        public Guid? CompanyAddressId { get; set; }
        public CompanyAddress CompanyAddress { get; set; }
    }
}