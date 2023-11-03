using SocialMediaApp.Models;

namespace SocialMediaApp.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Login(string username, string password);
        public Task<User> Register(User user);
        public Task<bool> UserExists(string username);
        public Task<ICollection<User>> GetUsers();

    }
}
