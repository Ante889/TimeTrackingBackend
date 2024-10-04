using Microsoft.Extensions.DependencyInjection;
using TimeTracking.App.Phase.Domain.Interface;
using TimeTracking.App.Phase.Infrastructure.Repository;
using TimeTracking.App.Phase.Infrastructure.Service;

namespace TimeTracking.App.Phase
{
    public static class PhaseServiceExtensions
    {
        public static void AddPhaseServices(this IServiceCollection services)
        {
            services.AddScoped<PhaseRepositoryInterface, PhaseRepository>();
            services.AddTransient<PhaseCalculationService>();
        }
    }
}
