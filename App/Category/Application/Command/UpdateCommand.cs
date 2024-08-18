using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Category.Application.Command
{
    public class UpdateCommand : IRequest<CategoryEntity?>
    {

        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal PricePerHour { get; set; }

        public UpdateCommand(int id, string name, decimal pricePerHour)
        {
            Id = id;
            Name = name;
            PricePerHour = pricePerHour;
        }
    }
}
