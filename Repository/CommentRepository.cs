using SocialMediaApp.Data;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Models;

namespace SocialMediaApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> createComment(Comment comment)
        {
            await _context.AddAsync(comment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
