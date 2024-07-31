using TimeTracking.App.Person.Domain.Entity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeTracking.App.Person.Domain.Interface;
using TimeTracking.App.Person.Infrastructure.Repository;

namespace TimeTracking.App.Person.Application.Query
{
    public class FindPersonByEmailQueryHandler : IRequestHandler<FindPersonByEmailQuery, PersonEntity?>
    {
        private readonly PersonRepositoryInterface _personRepository;

        public FindPersonByEmailQueryHandler(PersonRepositoryInterface personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonEntity?> Handle(FindPersonByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _personRepository.GetByEmailAsync(request.Email);          
        }
    }
}
