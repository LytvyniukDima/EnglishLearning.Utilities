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
        private readonly MongoCollectionSettings _collectionSettings;
        
        public MongoContext(IOptions<MongoConfiguration> mongoConfiguration, MongoCollectionNamesProvider mongoCollectionNamesProvider)
        {
            _mongoDbConfiguration = mongoConfiguration.Value;
            _mongoCollectionNamesProvider = mongoCollectionNamesProvider;
            _collectionSettings = new MongoCollectionSettings();
            
            var client = new MongoClient(_mongoDbConfiguration.ServerAddress);
            _database = client.GetDatabase(_mongoDbConfiguration.DatabaseName);
        }

        internal MongoCollectionSettings CollectionSettings => _collectionSettings; 
        
        public IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = _mongoCollectionNamesProvider.GetCollectionName<T>();
            return _database.GetCollection<T>(collectionName, _collectionSettings);
        }

        internal void OnDatabaseCreated(Action<MongoContextOptionsBuilder> mongoContextOptions)
        {
            var mongoContextOptionsBuilder = new MongoContextOptionsBuilder(this);
            mongoContextOptions?.Invoke(mongoContextOptionsBuilder);
        }
    }
}
