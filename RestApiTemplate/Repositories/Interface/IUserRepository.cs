using RestApiTemplate.Models;

namespace RestApiTemplate.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task<User?> GetByEmailAsync(string user);
    }
}
