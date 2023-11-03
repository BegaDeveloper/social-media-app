using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Models;

namespace SocialMediaApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user)
        {
            string passwordHash, passwordSalt;

            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }

        public bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt) {
            passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            passwordHash = BCrypt.Net.BCrypt.HashPassword(password, passwordSalt);
        }

        public async Task<bool> UserExists(string username)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Username == username);
            return userExists ? true : false;
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _context.Users.Include(u => u.Posts).ThenInclude(p => p.Comments).ToListAsync();
        }
    }
}
