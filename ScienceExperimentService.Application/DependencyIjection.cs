using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ScienceExperimentService.Application
{
    public static class DependencyIjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
