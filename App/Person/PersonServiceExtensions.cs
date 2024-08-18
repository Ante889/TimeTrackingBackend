using Microsoft.Extensions.DependencyInjection;
using TimeTracking.App.Base.Controllers;
using TimeTracking.App.Person.Domain.Interface;
using TimeTracking.App.Person.Infrastructure.Repository;
using TimeTracking.App.Person.Infrastructure.Service;

namespace TimeTracking.App.Person
{
    public static class PersonServiceExtensions
    {
        public static void AddPersonServices(this IServiceCollection services)
        {
            services.AddScoped<PersonRepositoryInterface, PersonRepository>();
            services.AddSingleton<JwtTokenService>();
            services.AddScoped<SafeExecutorInterface, SafeExecutor>();
        }
    }
}
