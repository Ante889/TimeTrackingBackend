using Microsoft.EntityFrameworkCore;
using TimeTracking.App.Project.Domain.Interface;
using TimeTracking.App.Project.Domain.Entity;
using TimeTracking.App.Base;
using TimeTracking.App.Person.Domain.Entity;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Project.Infrastructure.Repository;
public class ProjectRepository : ProjectRepositoryInterface
{
    private readonly TimeTrackingContext _context;
    private readonly DbSet<ProjectEntity> _projects;

    public ProjectRepository(TimeTrackingContext context)
    {
        _context = context;
        _projects = _context.Set<ProjectEntity>();
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        return await _projects.ToListAsync();
    }

    public async Task<ProjectEntity?> GetByIdAsync(int id)
    {
        return await _projects.FindAsync(id);
    }

    public async Task<IEnumerable<object>> GetByPersonAsync(PersonEntity person)
    {
        return await (from p in _projects
            where p.UserCreated == person.Id
            join ph in _context.Set<PhaseEntity>() on p.Id equals ph.Project into phases
            select new
            {
                Project = p,
                TotalAmountPaid = phases.Sum(ph => ph.AmountPaid ?? 0), 
                TotalAmountUnpaid = phases.Sum(ph => (ph.AmountPaid == null ? 0 : ph.AmountPaid.Value))
            })
            .ToListAsync();
    }



    public async Task AddAsync(ProjectEntity project)
    {
        await _projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProjectEntity project)
    {
        _projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var project = await _projects.FindAsync(id);
        if (project != null)
        {
            _projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}