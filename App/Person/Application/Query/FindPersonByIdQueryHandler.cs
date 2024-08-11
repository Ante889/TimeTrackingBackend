using TimeTracking.App.Person.Domain.Entity;
using MediatR;
using TimeTracking.App.Person.Domain.Interface;

namespace TimeTracking.App.Person.Application.Query
{
    public class FindPersonByIdQueryHandler : IRequestHandler<FindPersonByIdQuery, PersonEntity?>
    {
        private readonly PersonRepositoryInterface _personRepository;

        public FindPersonByIdQueryHandler(PersonRepositoryInterface personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonEntity?> Handle(FindPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _personRepository.GetByIdAsync(Int32.Parse(request.Id));          
        }
    }
}
