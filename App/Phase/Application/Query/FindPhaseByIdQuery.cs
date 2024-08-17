using MediatR;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Phase.Application.Query
{
    public class FindPhaseByIdQuery : IRequest<PhaseEntity?>
    {
        public int Id { get; set; }

        public FindPhaseByIdQuery(int id)
        {
            Id = id;
        }
    }
}