using App.Core.Interfaces;

namespace App.Core.Services
{
    public class OperationService<T> : IOperationService<T> where T : class
    {
        private readonly IMongoRepository<T> _repository;

        public OperationService(IMongoRepository<T> repository)
        {
            _repository = repository;
        }

        public async void Add(T request)
        {
            await _repository.AddAsync(request);
        }

        public Task CreateEntityAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindEntitiesAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEntityAsync(T entity)
        {
            throw new NotImplementedException();
        }

        //public void Add(T request)
        //{
        //    mongoRepository.InsertOne(request);
        //}

        //public async Task<Provider> GetProductByIdAsync(ObjectId id)
        //{
        //    return await _providerRepository.GetByIdAsync(id);
        //}
    }
}
