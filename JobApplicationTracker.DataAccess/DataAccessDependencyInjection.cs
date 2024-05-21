using JobApplicationTracker.DataAccess.DbContexts;
using JobApplicationTracker.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicationTracker.DataAccess
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration config)
        {
            AddDbContext(services, config);
            RegisterRepositories(services);
            return services;
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(
                opt => opt.UseSqlServer(config.GetConnectionString("Default")));
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
        }
    }
}
