using EnglishLearning.Utilities.Identity.Abstractions;
using EnglishLearning.Utilities.Identity.DelegationHandlers;
using EnglishLearning.Utilities.Identity.Filters;
using EnglishLearning.Utilities.Identity.Interfaces;
using EnglishLearning.Utilities.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Utilities.Identity.Configuration
{
    public static class IdentityServicesConfiguration
    {
        public static IServiceCollection AddEnglishLearningIdentity(this IServiceCollection services)
        {
            services.AddScoped<IJwtInfoProvider, JwtInfoProvider>();
            services.AddSingleton<IJwtSecretKeyProvider, JwtSecretKeyProvider>();
            services.AddSingleton<IAuthInfoProvider, AuthInfoProvider>();

            services.AddTransient<JwtInfoHeaderHandler>();
            
            return services;
        }

        public static MvcOptions AddEnglishLearningIdentityFilters(this MvcOptions options)
        {
            options.Filters.Add(typeof(JwtFilter));
            options.Filters.Add(typeof(EndpointAccessFilter));
            
            return options;
        }
    }
}