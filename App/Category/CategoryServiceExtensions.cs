using TimeTracking.App.Category.Domain.Interface;
using TimeTracking.App.Category.Infrastructure.Repository;

namespace TimeTracking.App.Category
{
    public static class CategoryServiceExtensions
    {
        public static void AddCategoryServices(this IServiceCollection services)
        {
            services.AddScoped<CategoryRepositoryInterface, CategoryRepository>();
        }
    }
}
