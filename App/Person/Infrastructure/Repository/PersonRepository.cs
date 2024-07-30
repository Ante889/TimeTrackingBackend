using Microsoft.EntityFrameworkCore;
using TimeTracking.App.Person.Domain.Interface;
using TimeTracking.App.Program.Domain.Entity;

namespace TimeTracking.App.Person.Infrastructure.Repository;
public class PersonRepository : PersonRepositoryInterface
{
    private readonly DbContext _context;
    private readonly DbSet<PersonEntity> _persons;

    public PersonRepository(DbContext context)
    {
        _context = context;
        _persons = _context.Set<PersonEntity>();
    }

    public async Task<IEnumerable<PersonEntity>> GetAllAsync()
    {
        return await _persons.ToListAsync();
    }

    public async Task<PersonEntity?> GetByIdAsync(int id)
    {
        return await _persons.FindAsync(id);
    }

    public async Task AddAsync(PersonEntity person)
    {
        await _persons.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PersonEntity person)
    {
        _persons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var person = await _persons.FindAsync(id);
        if (person != null)
        {
            _persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}