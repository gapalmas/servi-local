using App.Core.Interfaces.Core;
using System.Linq.Expressions;

namespace App.Core.Interfaces.Infrastructure
{
    public interface IRepository<T> where T : IDocument
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);
        T FindOne(Expression<Func<T, bool>> filterExpression);
        Task InsertOneAsync(T document);
        void InsertOne(T document);
        void ReplaceOne(T document);
        Task ReplaceOneAsync(T document);
        T FindById(string id);
        Task<T> FindByIdAsync(string id);
        void InsertMany(ICollection<T> documents);
        Task InsertManyAsync(ICollection<T> documents);
        void DeleteOne(Expression<Func<T, bool>> filterExpression);
        Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression);
        void DeleteById(string id);
        Task DeleteByIdAsync(string id);
        void DeleteMany(Expression<Func<T, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression);
    }
}
