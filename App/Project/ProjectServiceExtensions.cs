using TimeTracking.App.Person.Domain.Interface;
using TimeTracking.App.Person.Infrastructure.Repository;
using TimeTracking.App.Person.Infrastructure.Service;
using TimeTracking.App.Project.Domain.Interface;
using TimeTracking.App.Project.Infrastructure.Repository;

namespace TimeTracking.App.Project
{
    public static class ProjectServiceExtensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<PersonRepositoryInterface, PersonRepository>();
            services.AddSingleton<JwtTokenService>();
            services.AddScoped<ProjectRepositoryInterface, ProjectRepository>();
        }
    }
}
