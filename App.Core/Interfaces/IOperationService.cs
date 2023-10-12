using App.Core.Entities;

namespace App.Core.Interfaces
{
    public interface IOperationService<T> where T : class
    {
        Task GetAllAsync();
        void InsertOneAsync(T entity);
    }
}