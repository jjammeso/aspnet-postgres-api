using Microsoft.EntityFrameworkCore;
using RestApiTemplate.Data.Postgres;
using RestApiTemplate.DTOs;
using RestApiTemplate.Models;

namespace RestApiTemplate.Repositories.Postgres
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);

            return new UserDTO(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            IEnumerable<UserDTO> users = await _context.Users.Select(u => new UserDTO(u)).ToListAsync(); 
            return users;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
