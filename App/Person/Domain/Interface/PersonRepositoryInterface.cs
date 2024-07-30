
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracking.App.Program.Domain.Entity;

namespace TimeTracking.App.Person.Domain.Interface
{
    public interface PersonRepositoryInterface
    {
        Task<IEnumerable<PersonEntity>> GetAllAsync();
        Task<PersonEntity?> GetByIdAsync(int id);
        Task AddAsync(PersonEntity person);
        Task UpdateAsync(PersonEntity person);
        Task DeleteAsync(int id);
    }
}
