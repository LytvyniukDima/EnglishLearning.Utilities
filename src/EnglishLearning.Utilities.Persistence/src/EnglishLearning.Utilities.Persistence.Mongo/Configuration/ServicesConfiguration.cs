using System;
using EnglishLearning.Utilities.Configurations.MongoConfiguration;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EnglishLearning.Utilities.Persistence.Mongo.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddMongoContext(
            this IServiceCollection services,
            Action<MongoContextOptionsBuilder> mongoContextOptions = null)
        {
            services.AddSingleton<MongoContext>(sp =>
            {
                var configuration = sp.GetRequiredService<IOptions<MongoConfiguration>>();
                var mongoCollectionNameProvider = sp.GetRequiredService<MongoCollectionNamesProvider>();
                var mongoContext = new MongoContext(configuration, mongoCollectionNameProvider);
                mongoContext.OnDatabaseCreated(mongoContextOptions);
                
                return mongoContext;
            });
            
            return services;
        }

        public static IServiceCollection AddMongoCollectionNamesProvider(
            this IServiceCollection services,
            Action<MongoCollectionNamesProvider> mongoCollectionNames)
        {
            var mongoCollectionNamesProvider = new MongoCollectionNamesProvider();
            mongoCollectionNames.Invoke(mongoCollectionNamesProvider);

            services.AddSingleton<MongoCollectionNamesProvider>(mongoCollectionNamesProvider);

            return services;
        }
    }
}
