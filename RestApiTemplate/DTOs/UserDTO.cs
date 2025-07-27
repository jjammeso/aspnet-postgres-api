using RestApiTemplate.Models;

namespace RestApiTemplate.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public UserDTO(User? user)
        {
            Id = user?.Id.ToString();
            Name = user?.Name;
        }

    }
}
