using EnglishLearning.Utilities.Configurations.MongoConfiguration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EnglishLearning.Utilities.Persistence.Mongo.Contexts
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoConfiguration _mongoDbConfiguration;

        public MongoContext(IOptions<MongoConfiguration> mongoConfiguration)
        {
            _mongoDbConfiguration = mongoConfiguration.Value;

            var client = new MongoClient(_mongoDbConfiguration.ServerAddress);
            // TODO: Throw exception
            if (client != null)
                _database = client.GetDatabase(_mongoDbConfiguration.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
