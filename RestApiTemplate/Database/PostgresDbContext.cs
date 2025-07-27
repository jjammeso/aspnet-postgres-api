using RestApiTemplate.Models;
using Microsoft.EntityFrameworkCore;
using RestApiTemplate.Models.Postgres;

namespace RestApiTemplate.Database
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<PostgresRefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);

        }
    }
}
