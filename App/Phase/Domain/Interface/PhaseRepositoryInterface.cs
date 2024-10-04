using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Phase.Domain.Interface
{
    public interface PhaseRepositoryInterface
    {
        Task<PhaseEntity?> GetByIdAsync(int id);
        Task<IEnumerable<object>> GetByProjectAsync(ProjectEntity project);
        Task AddAsync(PhaseEntity phase);
        Task UpdateAsync(PhaseEntity phase);

        Task <int> CountAllOnProject(ProjectEntity project);
        Task DeleteAsync(int id);
    }
}
