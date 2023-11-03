using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApp.Dto;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("register")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto user) {
            if (user == null)
                return BadRequest(ModelState);

            if(await _userRepository.UserExists(user.UserName))
                return BadRequest("User already exists!");

            var mappedUser = _mapper.Map<User>(user);

            var createdUser = await _userRepository.Register(mappedUser);

            if (createdUser != null)
                return StatusCode(201);

            return BadRequest(ModelState);
          
        }

        [HttpPost("login")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user) {
            var userLogin = await _userRepository.Login(user.UserName, user.Password);
            if (userLogin == null)
                return Unauthorized();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                new Claim(ClaimTypes.Name, userLogin.Username)
            }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });


        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetUsers()
        {
            var usersEntities = await _userRepository.GetUsers();
            var users = _mapper.Map<List<UserDto>>(usersEntities);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
            
        }
    }
}
