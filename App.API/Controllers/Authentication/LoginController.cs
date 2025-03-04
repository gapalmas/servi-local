using App.Core.Helpers.Constants;
using App.Core.Interfaces.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IServiceFactory serviceFactory, IConfiguration configuration) : BaseController(serviceFactory)
    {
        private readonly IConfiguration configuration = configuration;

        [HttpPost]
        [Route("PostLoginDetails")]
        public ActionResult Login(string _userData = "result")
        {
            if (_userData != null)
            {
                var resultLoginCheck = _userData;
                if (resultLoginCheck == null)
                {
                    return BadRequest("Invalid Credentials");
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"] ?? ""),
                        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new(CommonConstants.UserId, ""),
                        new(CommonConstants.DisplayName, ""),
                        new("UserName", ""),
                        new("Email", "")
                    };


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"] ?? "",
                        configuration["Jwt:Audience"] ?? "",
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);


                    string result = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(result);
                }
            }
            else
            {
                return BadRequest("No Data Posted");
            }
        }
    }
}
