using MediatR;
using TimeTracking.App.Person.Domain.Entity;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Phase.Application.Query
{
    public class FindPhaseByProjectQuery : IRequest<IEnumerable<PhaseEntity>>
    {
        public ProjectEntity Project { get; }

        public FindPhaseByProjectQuery(ProjectEntity project)
        {
            Project = project;
        }
    }
}