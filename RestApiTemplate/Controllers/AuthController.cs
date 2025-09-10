using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RestApiTemplate.DTOs;
using RestApiTemplate.Services.Interfaces;
using RestApiTemplate.Validators;

namespace RestApiTemplate.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<UserRegisterDTO> _validator;

        public AuthController(IAuthService authService, IValidator<UserRegisterDTO> validator)
        {
            _authService = authService;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid) {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(ModelState);
            }
            var userDto = await _authService.RegisterAsync(dto);

            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDTO dto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var (accessToken, refreshToken) = await _authService.LoginAsync(dto, ipAddress);
                return Ok(new { accessToken, refreshToken });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO model)
        {

            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString()??"unknown";
                var (accessToken, refreshToken) = await _authService.RefreshTokenAsync(model.RefreshToken, ip);

                return Ok(new { accessToken, refreshToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

        }

    }
}
