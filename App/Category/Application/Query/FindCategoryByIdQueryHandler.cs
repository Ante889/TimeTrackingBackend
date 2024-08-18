using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Category.Domain.Interface;

namespace TimeTracking.App.Category.Application.Query
{
    public class FindCategoryByIdQueryHandler : IRequestHandler<FindCategoryByIdQuery, CategoryEntity?>
    {
        private readonly CategoryRepositoryInterface _categoryRepository;

        public FindCategoryByIdQueryHandler(CategoryRepositoryInterface categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryEntity?> Handle(FindCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByIdAsync(request.Id);
        }
    }
}
