using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Phase.Domain.Interface
{
    public interface PhaseRepositoryInterface
    {
        Task<PhaseEntity?> GetByIdAsync(int id);
        Task<IEnumerable<PhaseEntity>> GetByProjectAsync(ProjectEntity project);
        Task AddAsync(PhaseEntity phase);
        Task UpdateAsync(PhaseEntity phase);
        Task DeleteAsync(int id);
    }
}
