using Microsoft.EntityFrameworkCore;
using RestApiTemplate.Database;
using RestApiTemplate.DTOs;
using RestApiTemplate.Models;

namespace RestApiTemplate.Repositories.Postgres
{
    public class UserRepository:IUserRepository
    {
        private readonly PostgresDbContext _context;

        public UserRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync(); 
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
