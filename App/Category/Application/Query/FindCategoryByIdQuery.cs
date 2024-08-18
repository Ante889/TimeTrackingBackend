using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Category.Application.Query
{
    public class FindCategoryByIdQuery : IRequest<CategoryEntity?>
    {
        public int Id { get; set; }

        public FindCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}