using App.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IServiceFactory _serviceFactory;

        public BaseController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
    }
}
