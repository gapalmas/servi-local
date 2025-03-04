using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class ServiceFactory(IProviderService providerService, IUserService userService, IManagerService managerService, IMapper mapper) : IServiceFactory
    {
        protected readonly IManagerService managerService = managerService;
        protected readonly IMapper mapper = mapper;

        protected readonly IProviderService providerService = providerService;
        protected readonly IUserService userService = userService;

        public IProviderService ProviderService => providerService ?? new ProviderService(managerService, mapper);

        public IUserService UserService => userService ?? new UserService(managerService, mapper);
    }
}