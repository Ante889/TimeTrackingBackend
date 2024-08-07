using TimeTracking.App.Person.Domain.Entity;
using MediatR;
using TimeTracking.App.Person.Domain.Interface;

namespace TimeTracking.App.Person.Application.Command
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, PersonEntity?>
    {
        private readonly PersonRepositoryInterface _personRepository;

        public RegisterCommandHandler(PersonRepositoryInterface personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonEntity> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var personEntity = new PersonEntity
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password
            };

            await _personRepository.AddAsync(personEntity);

            return personEntity;
        }
    }
}
