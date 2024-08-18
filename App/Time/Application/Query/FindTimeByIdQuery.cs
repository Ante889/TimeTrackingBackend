using MediatR;
using TimeTracking.App.Time.Domain.Entity;

namespace TimeTracking.App.Time.Application.Query
{
    public class FindTimeByIdQuery : IRequest<TimeEntity?>
    {
        public int Id { get; set; }

        public FindTimeByIdQuery(int id)
        {
            Id = id;
        }
    }
}