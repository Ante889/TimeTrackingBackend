using MediatR;
using TimeTracking.App.Project.Domain.Interface;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Project.Application.Query
{
    public class FindProjectByUserQueryHandler : IRequestHandler<FindProjectByUserQuery, IEnumerable<ProjectEntity>>
    {
        private readonly ProjectRepositoryInterface _projectRepository;

        public FindProjectByUserQueryHandler(ProjectRepositoryInterface projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectEntity>> Handle(FindProjectByUserQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetByPersonAsync(request.Person);          
        }
    }
}
