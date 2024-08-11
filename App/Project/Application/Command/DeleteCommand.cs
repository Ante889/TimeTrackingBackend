using MediatR;
using TimeTracking.App.Person.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Project.Application.Command
{
    public class DeleteCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCommand(int id)
        {
            Id = id;
        }
    }
}
