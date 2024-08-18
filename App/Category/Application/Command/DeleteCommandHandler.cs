using MediatR;
using TimeTracking.App.Category.Domain.Interface;

namespace TimeTracking.App.Category.Application.Command
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly CategoryRepositoryInterface _categoryRepository;

        public DeleteCommandHandler(CategoryRepositoryInterface categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteAsync(request.Id);

            return true;
        }
    }
}
