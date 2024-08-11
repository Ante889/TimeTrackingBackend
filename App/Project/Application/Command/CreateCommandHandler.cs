using MediatR;
using TimeTracking.App.Project.Domain.Entity;
using TimeTracking.App.Project.Domain.Interface;

namespace TimeTracking.App.Project.Application.Command
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, ProjectEntity?>
    {
        private readonly ProjectRepositoryInterface _projectRepository;

        public CreateCommandHandler(ProjectRepositoryInterface projectRepositoryInterface)
        {
            _projectRepository = projectRepositoryInterface;
        }

        public async Task<ProjectEntity?> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var project = new ProjectEntity
            {
                Name = request.Name,
                Description = request.Description,
                UserCreated = request.UserCreated.Id,
                DateCreated = request.DateCreated
            };

            await _projectRepository.AddAsync(project);

            return project;
        }
    }
}
