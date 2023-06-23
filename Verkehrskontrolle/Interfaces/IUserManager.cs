using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Interfaces
{
    public interface IUserManager
    {

        Task<User?> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUserAsync();
        Task<string> Login(LoginCredentials credentials);
    }
}
