using System;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using MongoDB.Driver;

namespace EnglishLearning.Utilities.Persistence.Mongo.Configuration
{
    public class MongoContextOptionsBuilder
    {
        private readonly MongoContext _mongoContext;
        
        public MongoContextOptionsBuilder(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }
        
        public MongoContextOptionsBuilder HasIndex<T>(Action<IMongoIndexManager<T>> indexes)
        {
            var collection = _mongoContext.GetCollection<T>();
            indexes.Invoke(collection.Indexes);
            
            return this;
        }
    }
}
