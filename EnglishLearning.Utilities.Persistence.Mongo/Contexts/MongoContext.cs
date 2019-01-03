using System;
using EnglishLearning.Utilities.Configurations.MongoConfiguration;
using EnglishLearning.Utilities.Persistence.Mongo.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EnglishLearning.Utilities.Persistence.Mongo.Contexts
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoConfiguration _mongoDbConfiguration;
        private readonly MongoCollectionNamesProvider _mongoCollectionNamesProvider;
        
        public MongoContext(IOptions<MongoConfiguration> mongoConfiguration, MongoCollectionNamesProvider mongoCollectionNamesProvider)
        {
            _mongoDbConfiguration = mongoConfiguration.Value;
            _mongoCollectionNamesProvider = mongoCollectionNamesProvider;
            
            var client = new MongoClient(_mongoDbConfiguration.ServerAddress);
            // TODO: Throw exception
            if (client != null)
                _database = client.GetDatabase(_mongoDbConfiguration.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = _mongoCollectionNamesProvider.GetCollectionName<T>();
            return _database.GetCollection<T>(collectionName);
        }

        internal void OnDatabaseCreated(Action<MongoContextOptionsBuilder> mongoContextOptions)
        {
            var mongoContextOptionsBuilder = new MongoContextOptionsBuilder(this);
            mongoContextOptions?.Invoke(mongoContextOptionsBuilder);
        }
    }
}
