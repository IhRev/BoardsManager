using AutoMapper;
using BoardsManager.Users.Core.DTO;
using BoardsManager.Users.Domain.Entities;

namespace BoardsManager.Users.Application.Mappings
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}