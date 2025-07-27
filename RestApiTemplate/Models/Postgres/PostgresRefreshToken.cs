using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiTemplate.Models.Postgres
{
    public class PostgresRefreshToken
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Token { get; set; } = string.Empty;

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ExpiresAt { get; set; }

        public string CreatedByIp { get; set; } = string.Empty;

        public bool IsRevoked { get; set; } = false;

        public DateTime? RevokedAt { get; set; }
    }
}
