using FluentValidation;
using JobApplicationTracker.Application.Automapper;
using JobApplicationTracker.Application.Services;
using JobApplicationTracker.Application.Validators;
using JobApplicationTracker.DataAccess.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicationTracker.Application
{
    public static class ApplicationLayerDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            RegisterIdentity(services);
            ConfigureCookieSettings(services);
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

        private static void RegisterIdentity(IServiceCollection services)
        {
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void ConfigureCookieSettings(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.LoginPath = "/auth/login";
                options.AccessDeniedPath = "/error/forbidden";
            });
        }
    }
}
