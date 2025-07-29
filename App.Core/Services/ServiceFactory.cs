using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class ServiceFactory : IServiceFactory
    {
        public IProviderService ProviderService { get; }
        public IUserService UserService { get; }
        public IINEProcessorService INEProcessorService { get; }
        private readonly IManagerService ManagerService;
        private readonly IMapper Mapper;

        public ServiceFactory(
            IProviderService providerService,
            IUserService userService,
            IINEProcessorService iNEProcessorService,
            IManagerService managerService,
            IMapper mapper)
        {
            ProviderService = providerService;
            UserService = userService;
            INEProcessorService = iNEProcessorService;
            ManagerService = managerService;
            Mapper = mapper;
        }
    }
}