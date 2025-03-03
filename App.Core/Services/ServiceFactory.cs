using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class ServiceFactory : IServiceFactory
    {
        protected readonly IManagerService managerService;
        protected readonly IMapper mapper;

        protected readonly IProviderService providerService;
        protected readonly IUserService userService;

        public ServiceFactory(IProviderService providerService, IUserService userService, IManagerService managerService, IMapper mapper)
        {
            this.providerService = providerService;
            this.userService = userService;
            this.managerService = managerService;
            this.mapper = mapper;
        }

        public IProviderService ProviderService => providerService ?? new ProviderService(managerService, mapper);

        public IUserService UserService => userService ?? new UserService(managerService, mapper);
    }
}