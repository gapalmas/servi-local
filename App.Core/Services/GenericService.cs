using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;

namespace App.Core.Services
{
    public class GenericService<T> : IGenericService<T> where T : IDocument
    {
        private readonly IMongoRepository<T> _repository;

        public GenericService(IMongoRepository<T> repository)
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
