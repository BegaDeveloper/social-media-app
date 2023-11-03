using SocialMediaApp.Models;

namespace SocialMediaApp.Interfaces
{
    public interface ICommentRepository
    {
        public Task<bool> createComment(Comment comment);
    }
}
