using SocialMediaApp.Data;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Models;

namespace SocialMediaApp.Repository
{
    public class PostRepository : IPostRepository
        
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreatePost(Post post)
        {
            if (post == null)
                return false;

            await _context.AddAsync(post);
            var res = await _context.SaveChangesAsync();

            return res > 0 ? true : false;
            

        }
    }
}
