using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Time.Domain.Entity;

namespace TimeTracking.App.Time.Application.Query
{
    public class FindTimesByCategoryIdQuery : IRequest<IEnumerable<TimeEntity>>
    {
        public CategoryEntity Category { get; }

        public FindTimesByCategoryIdQuery(CategoryEntity category)
        {
            Category = category;
        }
    }
}