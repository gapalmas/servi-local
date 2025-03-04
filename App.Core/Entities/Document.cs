using App.Core.Interfaces.Core;
using MongoDB.Bson;

namespace App.Core.Entities
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
        public DateTime ModifiedAt => DateTime.UtcNow;
    }
}