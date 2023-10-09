using App.Core.Entities;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly OperationService<Provider> operationServiceProvider;

        public BaseController(OperationService<Provider> operationServiceProvider)
        {
            this.operationServiceProvider = operationServiceProvider;
        }
    }
}
