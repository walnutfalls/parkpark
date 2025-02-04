using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Auth.Core.Repositories.Interface;
using Mapster;
using Auth.Core.Models;

namespace Auth.Api.Controllers
{
    [Authorize]
	[ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        /// <summary>
        /// Return a given user
        /// </summary>
        /// <param name="id">userId</param>
        /// <returns>User object</returns>
        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> Index(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user.Adapt<Auth.Core.Models.User>());
        }

        /// <summary>
        /// Return a given user
        /// </summary>
        /// <param name="handle">handle</param>
        /// <returns>User object</returns>
        [HttpGet("handle/{handle}"), Authorize]
        public async Task<IActionResult> Handle(string handle)
        {
            var user = await _userRepository.GetUserByHandle(handle);
            if (user == null)
                return NotFound();

            return Ok(user.Adapt<Auth.Core.Models.User>());
        }
    }
}
