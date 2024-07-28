using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracking.App.Phase.Domain.Entity
{
    [Table("phase")]
    public class PhaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Project { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public string? Description { get; set; }

        [Required]
        public int PhaseNumber { get; set; }

        public decimal? AmountPaid { get; set; }
    }
}
