using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Category.Domain.Interface;

namespace TimeTracking.App.Category.Application.Query
{
    public class FindCategoriesByPhaseQueryHandler : IRequestHandler<FindCategoriesByPhaseQuery, IEnumerable<CategoryEntity>>
    {
        private readonly CategoryRepositoryInterface _categoryRepository;

        public FindCategoriesByPhaseQueryHandler(CategoryRepositoryInterface categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryEntity>> Handle(FindCategoriesByPhaseQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByPhaseAsync(request.Phase);          
        }
    }
}
