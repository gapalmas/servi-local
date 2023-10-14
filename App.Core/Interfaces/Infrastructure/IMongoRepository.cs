using System.Linq.Expressions;

namespace App.Core.Interfaces.Infrastructure
{
    public interface IMongoRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindOneAsync(Expression<Func<T, bool>> predicate);
        T FindOne(Expression<Func<T, bool>> filterExpression);
        Task InsertOneAsync(T entity);
        void InsertOne(T entity);
    }
}
