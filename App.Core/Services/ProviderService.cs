using App.Core.Dto.Request.Provider;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class ProviderService : IProviderService
    {
        protected readonly IServiceFactory _serviceFactory;
        public readonly IMapper mapper;

        public ProviderService(IServiceFactory _serviceFactory, IMapper mapper)
        {
            this._serviceFactory = _serviceFactory;
            this.mapper = mapper;
        }

        public void Create(ProviderRequestDto providerRequestDto)
        {
            if(providerRequestDto != null) 
            {
                _serviceFactory.OperationServiceProvider.InsertOneAsync(mapper.Map<Provider>(providerRequestDto));
            }
        }
    }
}
