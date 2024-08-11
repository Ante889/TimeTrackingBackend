using MediatR;
using TimeTracking.App.Person.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Project.Application.Query
{
    public class FindProjectByUserQuery : IRequest<IEnumerable<ProjectEntity>>
    {
        public PersonEntity Person { get; }

        public FindProjectByUserQuery(PersonEntity person)
        {
            Person = person;
        }
    }
}