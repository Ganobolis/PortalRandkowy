using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repositiory;

        public AuthController(IAuthRepository repositiory)
        {
            _repositiory = repositiory;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            username = username.ToLower();

            if(await _repositiory.UserExist(username))
                return BadRequest($"User {username} exist!");

            var userToCreate = new User
            {
                Username = username
            };
            var createdUser = await _repositiory.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}