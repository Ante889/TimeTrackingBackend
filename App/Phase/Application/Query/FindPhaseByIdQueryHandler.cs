using MediatR;
using TimeTracking.App.Project.Domain.Interface;
using TimeTracking.App.Project.Domain.Entity;
using TimeTracking.App.Phase.Domain.Interface;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Phase.Application.Query
{
    public class FindPhaseByIdQueryHandler : IRequestHandler<FindPhaseByIdQuery, PhaseEntity?>
    {
        private readonly PhaseRepositoryInterface _phaseRepository;

        public FindPhaseByIdQueryHandler(PhaseRepositoryInterface phaseRepository)
        {
            _phaseRepository = phaseRepository;
        }

        public async Task<PhaseEntity?> Handle(FindPhaseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _phaseRepository.GetByIdAsync(request.Id);
        }
    }
}
