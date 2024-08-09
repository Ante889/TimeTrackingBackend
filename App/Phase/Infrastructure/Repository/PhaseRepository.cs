using Microsoft.EntityFrameworkCore;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Base;
using TimeTracking.App.Phase.Domain.Interface;
using TimeTracking.App.Project.Domain.Entity;

namespace TimeTracking.App.Phase.Infrastructure.Repository
{
    public class PhaseRepository : PhaseRepositoryInterface
    {
        private readonly TimeTrackingContext _context;
        private readonly DbSet<PhaseEntity> _phases;

        public PhaseRepository(TimeTrackingContext context)
        {
            _context = context;
            _phases = _context.Set<PhaseEntity>();
        }


        public async Task<PhaseEntity?> GetByIdAsync(int id)
        {
            return await _phases.FindAsync(id);
        }

        public async Task<IEnumerable<PhaseEntity>> GetByProjectAsync(ProjectEntity project)
        {
            return await _phases
                .Where(p => p.Project == project.Id)
                .ToListAsync();
        }

        public async Task AddAsync(PhaseEntity phase)
        {
            await _phases.AddAsync(phase);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PhaseEntity phase)
        {
            _phases.Update(phase);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var phase = await _phases.FindAsync(id);
            if (phase != null)
            {
                _phases.Remove(phase);
                await _context.SaveChangesAsync();
            }
        }
    }
}
