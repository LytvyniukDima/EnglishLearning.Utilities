using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.Utilities.Persistence.Interfaces;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using MongoDB.Driver;

namespace EnglishLearning.Utilities.Persistence.Mongo.Repositories
{
    public abstract class BaseMongoRepository<T, TId> : IBaseRepository<T, TId> 
        where T : class, IEntity<TId>
    {
        protected readonly MongoContext _mongoContext;
        protected readonly IMongoCollection<T> _collection;

        protected BaseMongoRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
            _collection = _mongoContext.GetCollection<T>();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public virtual async Task<T> AddAsync(T item)
        {
            await _collection.InsertOneAsync(item);
            return item;
        }

        public virtual async Task<IReadOnlyList<T>> AddManyAsync(IReadOnlyList<T> items)
        {
            await _collection.InsertManyAsync(items);
            return items;
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            DeleteResult actionResult = await _collection.DeleteManyAsync(filter);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            var actionResult = await _collection.ReplaceOneAsync(x => x.Id.Equals(item.Id), item);
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        public virtual async Task<bool> DeleteAllAsync()
        {
            DeleteResult actionResult = await _collection.DeleteManyAsync(_ => true);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }
    }
}
