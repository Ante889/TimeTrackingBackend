using Microsoft.EntityFrameworkCore;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Base;
using TimeTracking.App.Phase.Domain.Interface;
using TimeTracking.App.Project.Domain.Entity;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Time.Domain.Entity;

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

        public async Task<int> CountAllOnProject(ProjectEntity project)
        {

            return await _phases.CountAsync(p => p.Project == project.Id);
        }

        public async Task<IEnumerable<object>> GetByProjectAsync(ProjectEntity project)
        {
            return await (from phase in _phases
                          where phase.Project == project.Id
                          join category in _context.Set<CategoryEntity>() on phase.Id equals category.Phase into categoryGroup
                          from category in categoryGroup.DefaultIfEmpty()
                          join time in _context.Set<TimeEntity>() on category.Id equals time.Category into timeGroup
                          from time in timeGroup.DefaultIfEmpty()
                          group new { time, category } by new
                          {
                              phase.Id,
                              phase.Project,
                              phase.DateCreated,
                              phase.Description,
                              phase.PhaseNumber,
                              phase.AmountPaid,
                              category.PricePerHour
                          } into phaseGroup
                          select new
                          {
                              phaseGroup.Key.Id,
                              phaseGroup.Key.Project,
                              phaseGroup.Key.DateCreated,
                              phaseGroup.Key.Description,
                              phaseGroup.Key.PhaseNumber,
                              phaseGroup.Key.AmountPaid,
                              TotalTimeInMinutes = phaseGroup.Sum(t => t.time != null ? t.time.TimeInMinutes : 0),
                              TotalCost = (phaseGroup.Sum(t => t.time != null ? t.time.TimeInMinutes : 0) / 60.0m) * (phaseGroup.Key.PricePerHour ?? 0)
                          })
                          .OrderBy(result => result.Id)
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
