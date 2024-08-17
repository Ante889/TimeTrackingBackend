using MediatR;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Phase.Application.Command
{
    public class UpdateCommand : IRequest<PhaseEntity?>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal? AmountPaid { get; set; }

        public UpdateCommand(
            int id,
            string? description,
            decimal? amountPaid
)
        {
            Id = id;
            AmountPaid = amountPaid;
            Description = description;
        }
    }
}
