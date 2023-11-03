using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Dto;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public CommentController(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> createComment([FromBody] Comment comment) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = await _commentRepository.createComment(comment);
            if (!result)
                return BadRequest("Error saving comment.");

            return Ok(comment);

        }
    }
}
