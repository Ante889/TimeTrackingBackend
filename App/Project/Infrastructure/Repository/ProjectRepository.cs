using Microsoft.EntityFrameworkCore;
using TimeTracking.App.Project.Domain.Interface;
using TimeTracking.App.Project.Domain.Entity;
using TimeTracking.App.Base;
using TimeTracking.App.Person.Domain.Entity;

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

    public async Task<IEnumerable<ProjectEntity>> GetByPersonAsync(PersonEntity person)
    {
        return await _projects
            .Where(p => p.UserCreated == person.Id)
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