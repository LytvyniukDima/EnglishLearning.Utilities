using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.Utilities.Persistence.Mongo.Interfaces
{
    public interface IStringIdEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}