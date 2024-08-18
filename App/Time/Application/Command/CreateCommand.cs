using System;
using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Time.Domain.Entity;

namespace TimeTracking.App.Time.Application.Command
{
    public class CreateCommand : IRequest<TimeEntity?>
    {
        public CategoryEntity Category { get; set; }
        public int TimeInMinutes { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public CreateCommand(
            CategoryEntity category,
            int timeInMinutes,
            string title,
            string? description = null
        )
        {
            Category = category;
            TimeInMinutes = timeInMinutes;
            Title = title;
            Description = description;
        }
    }
}
