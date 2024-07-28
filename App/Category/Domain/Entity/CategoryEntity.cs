using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracking.App.Category.Domain.Entity
{
    [Table("category")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }

        [Required]
        public int Phase { get; set; }

        public decimal? PricePerHour { get; set; }
    }
}