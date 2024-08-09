using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Category.Domain.Interface
{
    public interface CategoryRepositoryInterface
    {
        Task<CategoryEntity?> GetByIdAsync(int id);
        Task<IEnumerable<CategoryEntity>> GetByPhaseAsync(PhaseEntity phase);
        Task AddAsync(CategoryEntity category);
        Task UpdateAsync(CategoryEntity category);
        Task DeleteAsync(int id);
    }
}
