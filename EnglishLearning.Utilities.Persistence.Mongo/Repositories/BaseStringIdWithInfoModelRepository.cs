using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.Utilities.Persistence.Interfaces;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using EnglishLearning.Utilities.Persistence.Mongo.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EnglishLearning.Utilities.Persistence.Mongo.Repositories
{
    public abstract class BaseStringIdWithInfoModelRepository<T, TInfo> : IBaseWithInfoModelRepository<T, TInfo> where T: class, IStringIdEntity where TInfo: class
    {
        protected readonly MongoContext _dbContext;
        protected readonly IMongoCollection<T> _collection;

        protected abstract ProjectionDefinition<T, TInfo> InfoModelProjectionDefinition { get; }
        
        protected BaseStringIdWithInfoModelRepository(MongoContext dbContext, string collectionName)
        {
            _dbContext = dbContext;
            _collection = dbContext.GetCollection<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public virtual async Task AddAsync(T item)
        {
            await _collection.InsertOneAsync(item);
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            DeleteResult actionResult = await _collection.DeleteManyAsync(filter);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public virtual async Task<bool> UpdateAsync(string id, T item)
        {
            var actionResult = await _collection.ReplaceOneAsync(x => x.Id == id, item);
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        public virtual async Task<bool> DeleteAllAsync()
        {
            DeleteResult actionResult = await _collection.DeleteManyAsync(_ => true);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<TInfo>> GetAllInfoAsync()
        {
            var infoModels = await _collection
                .Find(new BsonDocument())
                .Project(InfoModelProjectionDefinition)
                .ToListAsync();

            return infoModels;
        }

        public async Task<IEnumerable<TInfo>> FindAllInfoAsync(Expression<Func<T, bool>> filter)
        {
            var infoModels = await _collection
                .Find(filter)
                .Project(InfoModelProjectionDefinition)
                .ToListAsync();

            return infoModels;
        }
    }
}
