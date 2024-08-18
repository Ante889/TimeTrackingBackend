using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Category.Domain.Interface;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Phase.Domain.Interface;

namespace TimeTracking.App.Category.Application.Command
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, CategoryEntity?>
    {
        private readonly CategoryRepositoryInterface _categoryRepository;

        public UpdateCommandHandler(CategoryRepositoryInterface categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryEntity?> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);

            if (category == null) return null;

            category.Name = request.Name;
            category.PricePerHour = request.PricePerHour;

            await _categoryRepository.UpdateAsync(category);

            return category;
        }
    }
}
