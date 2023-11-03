using SocialMediaApp.Dto;

namespace SocialMediaApp.Models
{
    public class Post
    {
        public int Id { get; set; } 
        public string Content {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
