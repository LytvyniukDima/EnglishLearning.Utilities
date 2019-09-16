using System;
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
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            _database.StringSet(key, value);
        }

        public byte[] GetByteValue(string key)
        {
            byte[] value = _database.StringGet(key);

            return value;
        }
        
        public void SetObjectValue<T>(string key, T value) 
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        
            var serializedObject = JsonConvert.SerializeObject(value);
            _database.StringSet(key, serializedObject);
        }

        public T GetObjectValue<T>(string key) 
            where T : class
        {
            string value = _database.StringGet(key);
            if (value == null)
            {
                return null;
            }

            var serializedObject = JsonConvert.DeserializeObject<T>(value);
            return serializedObject;
        }
    }
}
