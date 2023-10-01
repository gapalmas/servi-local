using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Core.Entities
{
    public class BaseCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public string? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int IsActive { get; set; }
    }
}
