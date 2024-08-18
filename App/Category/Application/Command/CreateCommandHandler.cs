using MediatR;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Category.Domain.Interface;

namespace TimeTracking.App.Category.Application.Command
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, CategoryEntity?>
    {
        private readonly CategoryRepositoryInterface _categoryRepository;

        public CreateCommandHandler(CategoryRepositoryInterface categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryEntity?> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var category = new CategoryEntity
            {
                Name = request.Name,
                PricePerHour = request.PricePerHour,
                Phase = request.Phase.Id
            };

            await _categoryRepository.AddAsync(category);

            return category;
        }
    }
}
