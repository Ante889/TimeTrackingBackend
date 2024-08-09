using TimeTracking.App.Person.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Project.Domain.Interface
{
    public interface ProjectRepositoryInterface
    {
        Task<IEnumerable<ProjectEntity>> GetAllAsync();
        Task<ProjectEntity?> GetByIdAsync(int id);
        Task<IEnumerable<ProjectEntity>> GetByPersonAsync(PersonEntity person);
        Task AddAsync(ProjectEntity project);
        Task UpdateAsync(ProjectEntity project);
        Task DeleteAsync(int id);
    }
}
