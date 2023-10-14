using App.Core.Interfaces;
using App.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IUserService userService;
        protected readonly IProviderService providerService;

        public BaseController(IProviderService providerService)
        {
            this.providerService = providerService;
        }

        public BaseController(IUserService userService)
        {
            userService = userService;
        }
    }
}
