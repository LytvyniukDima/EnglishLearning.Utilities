using EnglishLearning.Utilities.Persistence.Interfaces;
using EnglishLearning.Utilities.Persistence.Redis.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EnglishLearning.Utilities.Persistence.Redis.Configuration
{
    public static class RedisSettings
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration, string connectionStringKey = "RedisDatabase")
        {
            var redisConfiguration = configuration.GetRedisConfiguration(connectionStringKey);
            services.AddSingleton(redisConfiguration);
            
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));
            
            services.AddSingleton<IKeyValueRepository, RedisRepository>();
            
            return services;
        }
        
        private static RedisConfiguration GetRedisConfiguration(this IConfiguration configuration, string connectionStringKey)
        {
            var redisConfiguration = new RedisConfiguration();
            redisConfiguration.ConnectionString = configuration.GetConnectionString(connectionStringKey);
            
            return redisConfiguration;
        }
    }
}