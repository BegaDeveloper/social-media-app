using AutoMapper;
using SocialMediaApp.Dto;
using SocialMediaApp.Models;

namespace SocialMediaApp.Mapper
{
    public class Mapper : Profile
    {
        public Mapper() {
            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();   
        }
    }
}
