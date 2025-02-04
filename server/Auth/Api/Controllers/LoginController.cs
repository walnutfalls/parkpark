using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Auth.Core.Models;
using Auth.Core.Interfaces;
using Auth.Core;

namespace Auth.Api.Controllers
{    
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private ILogin _login;

        public LoginController(IConfiguration configuration, ILogin login)
        {
            _login = login;
            _configuration = configuration;
        }
		
		/// <summary>
		/// Login method that returns jwt.
		/// </summary>
		/// <param name="credentials"></param>
		/// <returns>Jwt token if credentials are correct.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Index([FromBody] Credentials credentials)
        {
            if(await _login.AreCredentialsValid(credentials))
            {
                return Ok(_login.CreateToken(credentials, _configuration["Domain"]));
            }

            return NotFound("Could not verify username and password");
        }
    }
}