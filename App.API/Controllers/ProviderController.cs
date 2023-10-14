using App.Core.Dto.Request.Provider;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ProviderController : BaseController
    {
        public ProviderController(IProviderService providerService) : base(providerService)
        {
        }

        [HttpPost]
        public ActionResult Add([FromBody] ProviderRequestDto providerRequestDto)
        {
            try
            {
                Log.Information("This is information");

                providerService.Create(providerRequestDto);

                Log.Error("This is error");
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //[HttpGet]
        //public ActionResult Get()
        //{
        //    Log.Information("This is information");
        //    try
        //    {
        //        var response = _serviceFactory.OperationServiceProvider.GetAllAsync();
        //        return Ok(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
