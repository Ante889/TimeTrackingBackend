
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracking.App.Person.Domain.Entity;

namespace TimeTracking.App.Person.Domain.Interface
{
    public interface PersonRepositoryInterface
    {
        Task<IEnumerable<PersonEntity>> GetAllAsync();
        Task<PersonEntity?> GetByIdAsync(int id);
        Task<PersonEntity?> GetByEmailAsync(string? email);
        Task AddAsync(PersonEntity person);
        Task UpdateAsync(PersonEntity person);
        Task DeleteAsync(int id);
    }
}
