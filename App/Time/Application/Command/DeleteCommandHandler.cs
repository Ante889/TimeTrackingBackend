using MediatR;
using TimeTracking.App.Phase.Domain.Interface;
using TimeTracking.App.Time.Domain.Interface;

namespace TimeTracking.App.Time.Application.Command
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly TimeRepositoryInterface _timeRepository;

        public DeleteCommandHandler(TimeRepositoryInterface timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _timeRepository.DeleteAsync(request.Id);

            return true;
        }
    }
}
