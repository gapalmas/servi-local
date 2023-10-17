using App.Core.Dto.Request.Provider;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class ProviderService : IProviderService
    {
        protected readonly IManagerGenericService managerGenericService;
        public readonly IMapper mapper;

        public ProviderService(IManagerGenericService managerGenericService, IMapper mapper)
        {
            this.managerGenericService = managerGenericService;
            this.mapper = mapper;
        }

        public void Create(ProviderRequestDto providerRequestDto)
        {
            if(providerRequestDto != null) 
            {
                managerGenericService.OperationServiceProvider.InsertOneAsync(mapper.Map<Provider>(providerRequestDto));
            }
        }
    }
}
