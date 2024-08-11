using MediatR;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Project.Application.Query
{
    public class FindProjectByIdQuery : IRequest<ProjectEntity?>
    {
        public int Id { get; set; }

        public FindProjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}