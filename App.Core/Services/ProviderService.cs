using App.Core.Dto.Request.Provider;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class ProviderService : IProviderService
    {
        protected readonly IManagerService managerGenericService;
        public readonly IMapper mapper;

        public ProviderService(IManagerService managerGenericService, IMapper mapper)
        {
            this.managerGenericService = managerGenericService;
            this.mapper = mapper;
        }

        public void Create(ProviderRequestDto providerRequestDto)
        {
            if(providerRequestDto != null) 
            {
                managerGenericService.GenericServiceProvider.InsertOneAsync(mapper.Map<Provider>(providerRequestDto));
            }
        }
    }
}
