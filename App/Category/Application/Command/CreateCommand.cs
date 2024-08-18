using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Category.Application.Command
{
    public class CreateCommand : IRequest<CategoryEntity?>
    {
        public PhaseEntity Phase { get; set; }
        public string Name { get; set; }
        public decimal PricePerHour { get; set; }

        public CreateCommand(PhaseEntity phase, string name, decimal pricePerHour)
        {
            Phase = phase;
            Name = name;
            PricePerHour = pricePerHour;
        }
    }
}
