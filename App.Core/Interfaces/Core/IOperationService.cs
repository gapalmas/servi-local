using App.Core.Entities;

namespace App.Core.Interfaces.Core
{
    public interface IOperationService<T> where T : IDocument
    {
        Task GetAllAsync();
        void InsertOneAsync(T entity);
    }
}