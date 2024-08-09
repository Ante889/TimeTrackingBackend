using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Time.Domain.Entity;

namespace TimeTracking.App.Time.Domain.Interface
{
    public interface TimeRepositoryInterface
    {
        Task<TimeEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TimeEntity>> GetByCategoryAsync(CategoryEntity category);
        Task AddAsync(TimeEntity time);
        Task UpdateAsync(TimeEntity time);
        Task DeleteAsync(int id);
    }
}
