using EnglishLearning.Utilities.Persistence.Interfaces;
using Newtonsoft.Json;
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
        
        public void SetByteValue(string key, byte[] value)
        {
            _database.StringSet(key, value);
        }

        public byte[] GetByteValue(string key)
        {
            byte[] value = _database.StringGet(key);

            return value;
        }
        
        public void SetObjectValue<T>(string key, T value)
        {
            var serializedObject = JsonConvert.SerializeObject(value);
            _database.StringSet(key, serializedObject);
        }

        public T GetObjectValue<T>(string key)
        {
            string value = _database.StringGet(key);
            var serializedObject = JsonConvert.DeserializeObject<T>(value);

            return serializedObject;
        }
    }
}
