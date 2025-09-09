using Microsoft.IdentityModel.Tokens;
using RestApiTemplate.DTOs;
using RestApiTemplate.Models;
using RestApiTemplate.Repositories.Interface;
using RestApiTemplate.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestApiTemplate.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthService(IConfiguration configuration, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<UserDTO> RegisterAsync(UserRegisterDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Id = Guid.NewGuid(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            var returnedUser = await _userRepository.CreateAsync(user);

            return new UserDTO(returnedUser);
        }
        public async Task<(string token, string refreshToken)> LoginAsync(UserLoginDTO dto, string ipAddress)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            var token = GenerateToken(user.Id.ToString()!);
            var refreshToken = GenerateRefreshToken(user.Id!, ipAddress);
            await _refreshTokenRepository.AddAsync(refreshToken);


            return (token, refreshToken.Token);
        }
        public string GenerateToken(String userId)
        {
            var secret = _configuration["JwtSettings:Secret"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expiryMinutes = int.Parse(_configuration["JwtSettings:ExpiryMinutes"]!);

            var claims = new[]
            {
                new Claim("UserID", userId),
            };

            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
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

        public RefreshToken GenerateRefreshToken(Guid userId, string ip)
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = userId,
                CreatedByIp = ip,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }
        public async Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string oldRefreshToken, string requestIp)
        {
            var tokenInDb = await _refreshTokenRepository.GetByTokenAsync(oldRefreshToken);

            if (tokenInDb == null || tokenInDb.IsRevoked || tokenInDb.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid refresh token");

            if (tokenInDb.CreatedByIp != requestIp)
                throw new UnauthorizedAccessException("IP mismatch");

            await _refreshTokenRepository.RevokeAsync(oldRefreshToken);

            var newRefreshToken = GenerateRefreshToken(tokenInDb.UserId, requestIp);
            await _refreshTokenRepository.AddAsync(newRefreshToken);

            var newAccessToken = GenerateToken(tokenInDb.UserId.ToString());

            return (newAccessToken, newRefreshToken.Token);
        }

    }
}
