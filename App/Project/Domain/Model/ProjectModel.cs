using System.ComponentModel.DataAnnotations;

namespace TimeTracking.App.Project.Domain.Model
{
    public class ProjectModel
    {
        [Required(ErrorMessage = "User is required")]
        public int UserCreated { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters")]
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
