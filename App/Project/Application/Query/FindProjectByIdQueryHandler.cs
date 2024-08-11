using MediatR;
using TimeTracking.App.Project.Domain.Interface;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Project.Application.Query
{
    public class FindProjectByIdQueryHandler : IRequestHandler<FindProjectByIdQuery, ProjectEntity?>
    {
        private readonly ProjectRepositoryInterface _projectRepository;

        public FindProjectByIdQueryHandler(ProjectRepositoryInterface projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectEntity?> Handle(FindProjectByIdQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetByIdAsync(request.Id);
        }
    }
}
