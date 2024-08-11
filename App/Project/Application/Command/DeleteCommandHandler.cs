using MediatR;
using TimeTracking.App.Project.Domain.Interface;

namespace TimeTracking.App.Project.Application.Command
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly ProjectRepositoryInterface _projectRepository;

        public DeleteCommandHandler(ProjectRepositoryInterface projectRepositoryInterface)
        {
            _projectRepository = projectRepositoryInterface;
        }

        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _projectRepository.DeleteAsync(request.Id);

            return true;
        }
    }
}
