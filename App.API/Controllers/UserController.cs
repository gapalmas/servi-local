using App.Core.Dto.Request.User;
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
    public class UserController(IServiceFactory serviceFactory) : BaseController(serviceFactory)
    {
        [HttpPost("register")]
        public ActionResult Create([FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                Log.Information("This is information");

                serviceFactory.UserService.Create(userRequestDto);

                Log.Error("This is error");
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("get-user")]
        public async Task<IActionResult> FindOne([FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                var response = await serviceFactory.UserService.FindOneAsync(userRequestDto);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}