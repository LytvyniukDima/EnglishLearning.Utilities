using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Utilities.Configurations.MongoConfiguration
{
    public static class MongoSettings
    {
        public static IServiceCollection AddMongoConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MongoConfiguration>(configuration.GetSection("MongoConfiguration"));
            
            return services;
        }
    }
}