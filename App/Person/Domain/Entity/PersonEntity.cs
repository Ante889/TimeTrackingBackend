using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracking.App.Person.Domain.Entity
{
    [Table("person")]
    public class PersonEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public required string Password { get; set; }

        [StringLength(20)]
        public string? Language { get; set; }
    }
}
