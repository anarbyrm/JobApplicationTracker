using JobApplicationTracker.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicationTracker.Application
{
    public static class ApplicationLayerDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            RegisterServices(services);
            return services;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IJobApplicationService, JobApplicationService>();
        }
    }
}
