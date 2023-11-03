using SocialMediaApp.Models;

namespace SocialMediaApp.Interfaces
{
    public interface IPostRepository
    {
        public Task<bool> CreatePost(Post post);
    }
}
