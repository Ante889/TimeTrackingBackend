using MediatR;
using TimeTracking.App.Person.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Project.Application.Command
{
    public class CreateCommand : IRequest<ProjectEntity?>
    {
        public PersonEntity UserCreated { get; set; }

        public DateTime DateCreated { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public CreateCommand(
            PersonEntity personEntity,
            DateTime dateCreated,
            string name,
            string? description
)
        {
            UserCreated = personEntity;
            DateCreated = dateCreated;
            Name = name;
            Description = description;
        }
    }
}
