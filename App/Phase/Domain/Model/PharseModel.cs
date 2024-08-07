using System.ComponentModel.DataAnnotations;

namespace TimeTracking.App.Phase.Domain.Model
{
    public class PhaseModel
    {
        [Required(ErrorMessage = "Project is required")]
        public int Project { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime DateCreated { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Phase number is required")]
        public int PhaseNumber { get; set; }

        public decimal? AmountPaid { get; set; }
    }
}
