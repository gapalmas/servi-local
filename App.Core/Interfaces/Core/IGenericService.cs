using App.Core.Entities;

namespace App.Core.Interfaces.Core
{
    public interface IGenericService<T> where T : IDocument
    {
        Task GetAllAsync();
        void InsertOneAsync(T entity);
    }
}