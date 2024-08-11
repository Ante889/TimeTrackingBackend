using MediatR;
using TimeTracking.App.Project.Domain.Entity;
using TimeTracking.App.Project.Domain.Interface;

namespace TimeTracking.App.Project.Application.Command
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, ProjectEntity?>
    {
        private readonly ProjectRepositoryInterface _projectRepository;

        public UpdateCommandHandler(ProjectRepositoryInterface projectRepositoryInterface)
        {
            _projectRepository = projectRepositoryInterface;
        }

        public async Task<ProjectEntity?> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project == null) return null;

            project.Name = request.Name;
            project.Description = request.Description;
            project.DateCreated = request.DateCreated;

            await _projectRepository.UpdateAsync(project);

            return project;
        }
    }
}
