using App.Core.Entities;
using App.Core.Interfaces;
using App.Core.Services;
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
