using System.ComponentModel.DataAnnotations;

namespace TimeTracking.App.Category.Domain.Model
{
    public class CategoryUpdateModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Price per hour is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price per hour must be a positive number")]
        public decimal PricePerHour { get; set; }
    }
}
