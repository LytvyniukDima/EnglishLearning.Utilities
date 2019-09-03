using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.Utilities.Persistence.Interfaces;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EnglishLearning.Utilities.Persistence.Mongo.Repositories
{
    public abstract class BaseMongoWithInfoModelRepository<T, TInfo, TId> : BaseMongoRepository<T, TId>, IBaseWithInfoModelRepository<T, TInfo, TId> where T: class, IEntity<TId> where TInfo: class
    {
        protected abstract ProjectionDefinition<T, TInfo> InfoModelProjectionDefinition { get; }
        
        protected BaseMongoWithInfoModelRepository(MongoContext dbContext): base(dbContext)
        {
            
        }

        public async Task<IReadOnlyList<TInfo>> GetAllInfoAsync()
        {
            var infoModels = await _collection
                .Find(new BsonDocument())
                .Project(InfoModelProjectionDefinition)
                .ToListAsync();

            return infoModels;
        }

        public async Task<IReadOnlyList<TInfo>> FindAllInfoAsync(Expression<Func<T, bool>> filter)
        {
            var infoModels = await _collection
                .Find(filter)
                .Project(InfoModelProjectionDefinition)
                .ToListAsync();

            return infoModels;
        }
    }
}
