using MediatR;
using TimeTracking.App.Person.Domain.Entity;

namespace TimeTracking.App.Person.Application.Query
{
    public class FindPersonByEmailQuery : IRequest<PersonEntity?>
    {
        public string? Email { get; }

        public FindPersonByEmailQuery(string? email)
        {
            Email = email;
        }
    }
}