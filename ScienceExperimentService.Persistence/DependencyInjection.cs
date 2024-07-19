using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScienceExperimentService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration["Dbconnection"];
            services.AddDbContext<ExperimentsDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IExperimentsDbContext>(provider =>
                provider.GetService<ExperimentsDbContext>());
            return services;
        }
    }
}
