using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Core.Interfaces.Core
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
        DateTime ModifiedAt { get; }
    }
}