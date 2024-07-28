using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracking.App.Time.Domain.Entity
{
    [Table("time")]
    public class TimeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required int Category { get; set; }

        [Required]
        public required int TimeInMinutes { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }

        public string? Description { get; set; }
    }
}