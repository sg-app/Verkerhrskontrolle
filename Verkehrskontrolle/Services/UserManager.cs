using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Extensions;
using Verkehrskontrolle.Interfaces;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Services
{
    public class UserManager : IUserManager
    {
        public const string INVALIDUSER = "Invalid User";
        public const string INVALIDPASSWORD = "Invalid Password";

        private readonly AuthDbContext _context;
        private readonly IJwtProvider _jwtProvider;

        public UserManager(AuthDbContext context, IJwtProvider jwtProvider)
        {
            _context = context;
            _jwtProvider = jwtProvider;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            var inDb = await _context.Users.FindAsync(user.Email);
            if (inDb != null)
                return null;
            
            user.Password = user.Password.CreateMD5();

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            var toDelete = await GetUserByEmailAsync(email);
            if(toDelete == null) return false;

            _context.Users.Remove(toDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FindAsync(email);
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            var toUpdate = await GetUserByEmailAsync(user.Email);
            if (toUpdate == null) return null;

            toUpdate.Password = user.Password.CreateMD5();
            _context.Users.Update(toUpdate);
            await _context.SaveChangesAsync();
            return toUpdate;
        }

        public async Task<string> Login(LoginCredentials credentials)
        {
            var user = await _context.Users.FindAsync(credentials.Email);
            if (user == null) { return INVALIDUSER; }
            if (user.Password != credentials.Password.CreateMD5()) { return INVALIDPASSWORD; }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
