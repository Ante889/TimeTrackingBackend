using MediatR;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Phase.Domain.Interface;

namespace TimeTracking.App.Phase.Application.Command
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, PhaseEntity?>
    {
        private readonly PhaseRepositoryInterface _phaseRepository;

        public CreateCommandHandler(PhaseRepositoryInterface phaseRepositoryInterface)
        {
            _phaseRepository = phaseRepositoryInterface;
        }

        public async Task<PhaseEntity?> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var phase = new PhaseEntity
            {
                AmountPaid = request.AmountPaid,
                Description = request.Description,
                Project = request.Project.Id,
                DateCreated = request.DateCreated,
                PhaseNumber = await _phaseRepository.CountAllOnProject(request.Project) + 1
            };

            await _phaseRepository.AddAsync(phase);

            return phase;
        }
    }
}
