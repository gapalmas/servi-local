using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class ServiceFactory : IServiceFactory
    {
        protected readonly IManagerGenericService managerGenericService;
        protected readonly IMapper mapper;

        protected readonly IProviderService providerService;
        protected readonly IUserService userService;

        public ServiceFactory(IProviderService providerService, IUserService userService, IManagerGenericService managerGenericService, IMapper mapper)
        {
            this.providerService = providerService;
            this.userService = userService;
            this.managerGenericService = managerGenericService;
            this.mapper = mapper;
        }

        public IProviderService ProviderService => providerService ?? new ProviderService(managerGenericService, mapper);

        public IUserService UserService => userService ?? new UserService(managerGenericService, mapper);
    }
}