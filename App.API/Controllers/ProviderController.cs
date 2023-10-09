using App.Core.Entities;
using App.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : BaseController
    {
        public ProviderController(OperationService<Provider> operationServiceProvider) : base(operationServiceProvider)
        {
        }

        [HttpPost]
        public ActionResult Add([FromBody] Provider provider)
        {
            try
            {
                operationServiceProvider.Add(provider);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
