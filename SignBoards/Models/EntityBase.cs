using System;
using System.ComponentModel.DataAnnotations;

namespace SignBoards.Models
{
    public class EntityBase
    {
        [Required]
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public Guid? UpdatedByUserId { get; set; }
    }
}