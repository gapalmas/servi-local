using App.Core.Entities;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
                Log.Information("This is information");
               
                operationServiceProvider.InsertOneAsync(provider);
                
                Log.Error("This is error");
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            Log.Information("Este es un mensaje de información");
            try
            {
                var response = operationServiceProvider.GetAllAsync();
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
