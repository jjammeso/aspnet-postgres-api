using Microsoft.AspNetCore.Mvc;
using RestApiTemplate.DTOs;
using RestApiTemplate.Services.Interfaces;

namespace RestApiTemplate.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO dto)
        {
            var userDto = await _authService.RegisterAsync(dto);

            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDTO dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
        }

    }
}
