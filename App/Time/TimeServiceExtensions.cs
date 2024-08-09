using TimeTracking.App.Project.Domain.Interface;
using TimeTracking.App.Time.Domain.Interface;
using TimeTracking.App.Time.Infrastructure.Repository;

namespace TimeTracking.App.Time
{
    public static class TimeServiceExtensions
    {
        public static void AddTimeServices(this IServiceCollection services)
        {
            services.AddScoped<TimeRepositoryInterface, TimeRepository>();
        }
    }
}
