using System.ComponentModel.DataAnnotations;

namespace TimeTracking.App.Phase.Domain.Model
{
    public class PhaseUpdateModel
    {
        public string? Description { get; set; }

        [Required(ErrorMessage = "Phase number is required")]

        public decimal? AmountPaid { get; set; }
    }
}
