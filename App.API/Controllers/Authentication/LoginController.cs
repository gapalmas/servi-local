using App.Core.Helpers.Constants;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IConfiguration configuration;

        public LoginController(IServiceFactory serviceFactory, IConfiguration configuration) : base(serviceFactory)
        {
            this.configuration = configuration;
        }

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
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"] ?? ""),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(CommonConstants.UserId, ""),
                        new Claim(CommonConstants.DisplayName, ""),
                        new Claim("UserName", ""),
                        new Claim("Email", "")
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
