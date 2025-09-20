using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiTemplate.DTOs;
using RestApiTemplate.Models;
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

        [Authorize(Roles = "Teacher")]
        [HttpGet(Name = "User")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("id/{id:guid}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpGet("email/{email}")]
        public async Task<User?> GetByEmail(string email)
        {
            return await _userService.GetByEmailAsync(email);
        }

    }
}
