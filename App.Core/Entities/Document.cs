using App.Core.Interfaces;
using MongoDB.Bson;

namespace App.Core.Entities
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
