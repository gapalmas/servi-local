using App.Core.Dto.Request.User;
using App.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IUserService userService) : base(userService) { }
        [HttpPost]
        public ActionResult Add([FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                //Log.Information("This is information");

                userService.Create(userRequestDto);

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
