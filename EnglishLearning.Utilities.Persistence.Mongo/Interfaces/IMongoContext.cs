using MongoDB.Driver;

namespace EnglishLearning.Utilities.Persistence.Mongo.Interfaces
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
