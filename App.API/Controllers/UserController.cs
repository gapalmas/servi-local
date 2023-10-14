using App.Core.Entities;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IServiceFactory serviceFactory) : base(serviceFactory) { }

        [HttpPost]
        public ActionResult Add([FromBody] User user )
        {
            try
            {
                Log.Information("This is information");

                _serviceFactory.OperationServiceUser.InsertOneAsync(user);

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
