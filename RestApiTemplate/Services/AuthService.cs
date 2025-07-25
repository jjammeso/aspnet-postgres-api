using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using RestApiTemplate.DTOs;
using RestApiTemplate.Models;
using RestApiTemplate.Repositories;
using RestApiTemplate.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace RestApiTemplate.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> RegisterAsync(UserRegisterDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Id = Guid.NewGuid().ToString(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            var returnedUser = await _userRepository.CreateAsync(user);

            return new UserDTO(returnedUser);
        }

        public async Task<string> LoginAsync(UserLoginDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return GenerateToken(user);
        }
        public string GenerateToken(User user)
        {
            var secret = _configuration["JwtSettings:Secret"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expiryMinutes = int.Parse(_configuration["JwtSettings:ExpiryMinutes"]!);

            var claims = new[]
            {
                new Claim("UserID", user.Id.ToString()),
            };

            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer,
                audience,
                claims,
                expires:DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
