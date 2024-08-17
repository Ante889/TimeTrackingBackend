using System.ComponentModel.DataAnnotations;
using MediatR;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Phase.Application.Command
{
    public class CreateCommand : IRequest<PhaseEntity?>
    {
        public ProjectEntity Project { get; set; }

        public DateTime DateCreated { get; set; }

        public string? Description { get; set; }

        public int PhaseNumber { get; set; }

        public decimal? AmountPaid { get; set; }

        public CreateCommand(
            ProjectEntity project,
            DateTime dateCreated,
            int phaseNumber,
            string? description,
            decimal? amountPaid
)
        {
            Project = project;
            DateCreated = dateCreated;
            PhaseNumber = phaseNumber;
            AmountPaid = amountPaid;
            Description = description;
        }
    }
}
