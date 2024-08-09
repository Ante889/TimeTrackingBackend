using Microsoft.EntityFrameworkCore;
using TimeTracking.App.Category.Domain.Interface;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Base;
using TimeTracking.App.Phase.Domain.Entity;

namespace TimeTracking.App.Category.Infrastructure.Repository
{
    public class CategoryRepository : CategoryRepositoryInterface
    {
        private readonly TimeTrackingContext _context;
        private readonly DbSet<CategoryEntity> _categories;

        public CategoryRepository(TimeTrackingContext context)
        {
            _context = context;
            _categories = _context.Set<CategoryEntity>();
        }

        public async Task<CategoryEntity?> GetByIdAsync(int id)
        {
            return await _categories.FindAsync(id);
        }

        public async Task<IEnumerable<CategoryEntity>> GetByPhaseAsync(PhaseEntity phase)
        {
            return await _categories
                .Where(c => c.Phase == phase.Id)
                .ToListAsync();
        }

        public async Task AddAsync(CategoryEntity category)
        {
            await _categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryEntity category)
        {
            _categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categories.FindAsync(id);
            if (category != null)
            {
                _categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
