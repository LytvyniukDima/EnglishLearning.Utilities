using EnglishLearning.Utilities.Persistence.Interfaces;
using StackExchange.Redis;

namespace EnglishLearning.Utilities.Persistence.Redis.Repositories
{
    public class RedisRepository : IKeyValueRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;

        public RedisRepository(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _database = redisConnection.GetDatabase();
        }
        
        public void SetStringKeyValue(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public string GetStringValueByKey(string key)
        {
            var value = _database.StringGet(key);

            return value;
        }
    }
}
