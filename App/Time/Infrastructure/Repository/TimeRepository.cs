using Microsoft.EntityFrameworkCore;
using TimeTracking.App.Time.Domain.Interface;
using TimeTracking.App.Time.Domain.Entity;
using TimeTracking.App.Base;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Person.Domain.Entity;

namespace TimeTracking.App.Time.Infrastructure.Repository
{
    public class TimeRepository : TimeRepositoryInterface
    {
        private readonly TimeTrackingContext _context;
        private readonly DbSet<TimeEntity> _times;

        public TimeRepository(TimeTrackingContext context)
        {
            _context = context;
            _times = _context.Set<TimeEntity>();
        }

        public async Task<TimeEntity?> GetByIdAsync(int id)
        {
            return await _times.FindAsync(id);
        }

        public async Task<IEnumerable<TimeEntity>> GetByCategoryAsync(CategoryEntity category)
        {
            return await _times
                .Where(t => t.Category == category.Id)
                .ToListAsync();
        }

        public async Task AddAsync(TimeEntity time)
        {
            await _times.AddAsync(time);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeEntity time)
        {
            _times.Update(time);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var time = await _times.FindAsync(id);
            if (time != null)
            {
                _times.Remove(time);
                await _context.SaveChangesAsync();
            }
        }
    }
}
