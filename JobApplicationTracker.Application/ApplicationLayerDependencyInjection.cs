using FluentValidation;
using FluentValidation.AspNetCore;
using JobApplicationTracker.Application.Automapper;
using JobApplicationTracker.Application.Services;
using JobApplicationTracker.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicationTracker.Application
{
    public static class ApplicationLayerDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            RegisterServices(services);
            RegisterMappingProfiles(services);
            RegisterValidators(services);
            return services;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IJobApplicationService, JobApplicationService>();
        }

        private static void RegisterMappingProfiles(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IMappingProfileMarker));
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<IValidatorMarker>();
        }
    }
}
