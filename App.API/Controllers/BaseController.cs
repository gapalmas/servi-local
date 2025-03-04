using App.Core.Interfaces.Core;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class BaseController(IServiceFactory serviceFactory) : ControllerBase
    {
        protected readonly IServiceFactory serviceFactory = serviceFactory;
    }
}