using App.Core.Dto.Request.Provider;
using App.Core.Interfaces.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProviderController(IServiceFactory serviceFactory) : BaseController(serviceFactory)
    {
        [HttpPost]
        public ActionResult Add([FromBody] ProviderRequestDto providerRequestDto)
        {
            try
            {
                Log.Information("This is information");

                serviceFactory.ProviderService.Create(providerRequestDto);

                Log.Error("This is error");
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}