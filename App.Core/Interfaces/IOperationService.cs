using App.Core.Entities;

namespace App.Core.Interfaces
{
    public interface IOperationService<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindEntitiesAsync(string searchTerm);
        Task CreateEntityAsync(T entity);
        Task UpdateEntityAsync(T entity);
        Task DeleteEntityAsync(Guid id);
    }
}