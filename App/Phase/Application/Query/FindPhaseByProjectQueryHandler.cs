using MediatR;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Phase.Domain.Interface;

namespace TimeTracking.App.Phase.Application.Query
{
    public class FindPhaseByProjectQueryHandler : IRequestHandler<FindPhaseByProjectQuery, IEnumerable<Object>>
    {
        private readonly PhaseRepositoryInterface _phaseRepository;

        public FindPhaseByProjectQueryHandler(PhaseRepositoryInterface phaseRepository)
        {
            _phaseRepository = phaseRepository;
        }

        public async Task<IEnumerable<Object>> Handle(FindPhaseByProjectQuery request, CancellationToken cancellationToken)
        {
            return await _phaseRepository.GetByProjectAsync(request.Project);          
        }
    }
}
