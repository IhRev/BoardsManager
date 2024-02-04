using AutoMapper;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.Dto;
using BoardsManager.Users.Domain.Entities;
using BoardsManager.Users.Domain.Repositories;

namespace BoardsManager.Users.Application.Services
{
    public class UserQueryService(IUserRepository userRepository, IMapper mapper) : IUserQueryService
    {
        public IEnumerable<UserDto> GetUsersByProjectId(string projectId)
        {
            IEnumerable<User> users = userRepository.GetUsersByProjectId(projectId);
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByIdAsync(string id) 
        {
            User? user = await userRepository.GetUserByIdAsync(id);
            return mapper.Map<UserDto>(user);
        }
    }
}