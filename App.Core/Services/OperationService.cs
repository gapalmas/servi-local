using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;

namespace App.Core.Services
{
    public class OperationService<T> : IOperationService<T> where T : class
    {
        private readonly IMongoRepository<T> _repository;

        public OperationService(IMongoRepository<T> repository)
        {
            _repository = repository;
        }

        public async void InsertOneAsync(T request)
        {
            await _repository.InsertOneAsync(request);
        }

        public async Task GetAllAsync()
        {
            await _repository.GetAllAsync();
        }
    }
}
