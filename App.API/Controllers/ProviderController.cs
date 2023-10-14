using App.Core.Entities;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProviderController : BaseController
    {
        public ProviderController(IServiceFactory serviceFactory) : base(serviceFactory) { }

        [HttpPost]
        public ActionResult Add([FromBody] Provider provider)
        {
            try
            {
                Log.Information("This is information");

                _serviceFactory.OperationServiceProvider.InsertOneAsync(provider);

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
                var response = _serviceFactory.OperationServiceProvider.GetAllAsync();
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
