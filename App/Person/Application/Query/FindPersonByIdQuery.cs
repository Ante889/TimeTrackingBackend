using MediatR;
using TimeTracking.App.Person.Domain.Entity;

namespace TimeTracking.App.Person.Application.Query
{
    public class FindPersonByIdQuery : IRequest<PersonEntity?>
    {
        public string Id { get; set; }

        public FindPersonByIdQuery(string id)
        {
            Id = id;
        }
    }
}