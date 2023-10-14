using App.Core.Entities;

namespace App.Core.Interfaces.Core
{
    public interface IOperationService<T> where T : class
    {
        Task GetAllAsync();
        void InsertOneAsync(T entity);
    }
}