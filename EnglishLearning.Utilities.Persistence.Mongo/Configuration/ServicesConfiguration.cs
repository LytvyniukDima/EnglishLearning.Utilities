using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Utilities.Persistence.Mongo.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddMongoDependencies(this IServiceCollection services)
        {
            services.AddScoped<MongoContext>();
            
            return services;
        }
    }
}
