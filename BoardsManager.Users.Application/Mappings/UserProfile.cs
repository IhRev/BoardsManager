using AutoMapper;
using BoardsManager.Users.Core.Dto;
using BoardsManager.Users.Domain.Entities;

namespace BoardsManager.Users.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}