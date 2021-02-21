using System.Collections.Generic;
using MongoDB.Bson;

namespace EnglishLearning.Utilities.Persistence.Mongo.Extensions
{
    public static class MongoExtensions
    {
        private static BsonArray ToBsonArray(this IEnumerable<BsonValue> values)
        {
            var array = new BsonArray();
            array.AddRange(values);

            return array;
        }
    }
}