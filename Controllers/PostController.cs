using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Dto;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public PostController(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostContent([FromQuery] int userId,[FromBody] PostDto postDto)
        {
            if(postDto.Content == null)
                return BadRequest(ModelState);

            var post = new Post 
            { 
                Content = postDto.Content,
                UserId = userId
            };

            var createdPost = await _postRepository.CreatePost(post);
            if (createdPost != null)
                return StatusCode(200);

            return BadRequest("Something went wrong!");

        }

    }
}
