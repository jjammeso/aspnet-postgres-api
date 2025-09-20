using System.ComponentModel.DataAnnotations;

namespace RestApiTemplate.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User";
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
