using MediatR;
using TimeTracking.App.Phase.Domain.Interface;

namespace TimeTracking.App.Phase.Application.Command
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly PhaseRepositoryInterface _phaseRepository;

        public DeleteCommandHandler(PhaseRepositoryInterface phaseRepositoryInterface)
        {
            _phaseRepository = phaseRepositoryInterface;
        }

        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _phaseRepository.DeleteAsync(request.Id);

            return true;
        }
    }
}
