using App.Core.Dto.Request.User;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        //public UserController(IProviderService providerService, IUserService userService) : base(providerService, userService)
        //{
        //}

        [HttpPost]
        public ActionResult Add([FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                //Log.Information("This is information");

                serviceFactory.UserService.Create(userRequestDto);

                //Log.Error("This is error");
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
