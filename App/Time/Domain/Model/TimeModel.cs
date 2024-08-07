using System.ComponentModel.DataAnnotations;

namespace TimeTracking.App.Time.Domain.Model
{
    public class TimeModel
    {
        [Required(ErrorMessage = "Category is required")]
        public int Category { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public int TimeInMinutes { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 255 characters")]
        public required string Title { get; set; }

        public string? Description { get; set; }
    }
}
