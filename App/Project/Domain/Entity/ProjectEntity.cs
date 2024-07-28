using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracking.App.Project.Domain.Entity
{
    [Table("project")]
    public class ProjectEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserCreated { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}