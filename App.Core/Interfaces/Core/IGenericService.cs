using App.Core.Entities;
using System.Linq.Expressions;

namespace App.Core.Interfaces.Core
{
    public interface IGenericService<T> where T : IDocument
    {
        T FindById(string id);
        T FindOne(Expression<Func<T, bool>> predicate);
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        void InsertOneAsync(T entity);
        void ReplaceOne(T entity);
    }
}