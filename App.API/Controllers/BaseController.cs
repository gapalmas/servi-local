using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IServiceFactory serviceFactory;

        public BaseController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        //protected readonly IUserService userService;
        //protected readonly IProviderService providerService;

        //public BaseController(IProviderService providerService, IUserService userService)
        //{
        //    this.providerService = providerService;
        //    this.userService = userService;
        //}
    }
}
