using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracking.App.Program.Domain.Entity
{
    [Table("person")]
    public class PersonEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(18)]
        public required string Password { get; set; }

        [MaxLength(20)]
        public string? Language { get; set; }
    }
}