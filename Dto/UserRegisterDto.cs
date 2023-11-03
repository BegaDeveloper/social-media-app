using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Dto
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be at least 4 characters!")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be at least 5 characters!")]
        public string Password { get; set; }
    }
}
