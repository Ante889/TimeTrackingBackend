using MediatR;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Phase.Domain.Interface;

namespace TimeTracking.App.Phase.Application.Command
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, PhaseEntity?>
    {
        private readonly PhaseRepositoryInterface _phaseRepository;

        public UpdateCommandHandler(PhaseRepositoryInterface phaseRepositoryInterface)
        {
            _phaseRepository = phaseRepositoryInterface;
        }

        public async Task<PhaseEntity?> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var phase = await _phaseRepository.GetByIdAsync(request.Id);

            if (phase == null) return null;

            phase.Description = request.Description;
            phase.AmountPaid = request.AmountPaid;

            await _phaseRepository.UpdateAsync(phase);

            return phase;
        }
    }
}
