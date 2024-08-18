using MediatR;
using TimeTracking.App.Time.Domain.Entity;

namespace TimeTracking.App.Time.Application.Command
{
    public class UpdateCommand : IRequest<TimeEntity?>
    {
        public int Id { get; set; }
        public int TimeInMinutes { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public UpdateCommand(
            int id,
            int timeInMinutes,
            string title,
            string? description = null
        )
        {
            Id = id;
            TimeInMinutes = timeInMinutes;
            Title = title;
            Description = description;
        }
    }
}
