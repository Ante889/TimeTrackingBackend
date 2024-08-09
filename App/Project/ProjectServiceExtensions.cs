using Microsoft.Extensions.DependencyInjection;
using TimeTracking.App.Project.Domain.Interface;
using TimeTracking.App.Project.Infrastructure.Repository;

namespace TimeTracking.App.Project
{
    public static class ProjectServiceExtensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<ProjectRepositoryInterface, ProjectRepository>();
        }
    }
}
