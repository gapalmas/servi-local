using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;
using System.Linq.Expressions;

namespace App.Core.Services
{
    public class RepositoryService<T> : IGenericService<T> where T : IDocument
    {
        private readonly IGenericRepository<T> _repository;

        public RepositoryService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async void InsertOneAsync(T request)
        {
            await _repository.InsertOneAsync(request);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public T FindById(string id) 
        {
            return _repository.FindById(id);
        }

        public async void ReplaceOne(T entity)
        {
            await _repository.ReplaceOneAsync(entity);
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.FindOneAsync(predicate);
        }

        public T FindOne(Expression<Func<T, bool>> predicate)
        {
            return _repository.FindOne(predicate);
        }
    }
}
