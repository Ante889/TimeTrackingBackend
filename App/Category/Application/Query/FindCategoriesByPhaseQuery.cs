using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Category.Application.Query
{
    public class FindCategoriesByPhaseQuery : IRequest<IEnumerable<CategoryEntity>>
    {
        public PhaseEntity Phase { get; }

        public FindCategoriesByPhaseQuery(PhaseEntity phase)
        {
            Phase = phase;
        }
    }
}