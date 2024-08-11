using MediatR;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Project.Application.Command
{
    public class UpdateCommand : IRequest<ProjectEntity?>
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public UpdateCommand(
            int id,
            DateTime dateCreated,
            string name,
            string? description
)
        {
            Id = id;
            DateCreated = dateCreated;
            Name = name;
            Description = description;
        }
    }
}
