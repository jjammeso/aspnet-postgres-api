using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiTemplate.DTOs;
using RestApiTemplate.Services.Interfaces;

namespace RestApiTemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService UserService)
        {
            _logger = logger;
            _userService = UserService;
        }

        [Authorize]
        [HttpGet(Name = "User")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
     
    }
}
