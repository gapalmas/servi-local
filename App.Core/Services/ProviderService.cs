using App.Core.Dto.Request.Provider;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class ProviderService : IProviderService
    {
        protected readonly IManagerService managerService;
        public readonly IMapper mapper;

        public ProviderService(IManagerService managerService, IMapper mapper)
        {
            this.managerService = managerService;
            this.mapper = mapper;
        }

        public void Create(ProviderRequestDto providerRequestDto)
        {
            if(providerRequestDto != null) 
            {
                managerService.ServiceProvider.InsertOneAsync(mapper.Map<Provider>(providerRequestDto));
            }
        }
    }
}
