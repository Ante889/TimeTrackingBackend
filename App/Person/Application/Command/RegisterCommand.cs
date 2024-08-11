using MediatR;
using TimeTracking.App.Person.Domain.Entity;

namespace TimeTracking.App.Person.Application.Command
{
    public class RegisterCommand : IRequest<PersonEntity?>
    {
        public string Email { get; }

        public string Password { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public RegisterCommand(
            string email, 
            string password,
            string firstName,
            string lastName
        )
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}