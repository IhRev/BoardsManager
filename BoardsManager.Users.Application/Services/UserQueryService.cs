using AutoMapper;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.DTO;
using BoardsManager.Users.Domain.Entities;
using BoardsManager.Users.Domain.Repositories;

namespace BoardsManager.Users.Application.Services
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserQueryService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public IEnumerable<UserDTO> GetUsersByProjectId(string projectId)
        {
            IEnumerable<User> users = userRepository.GetUsersByProjectId(projectId);
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserById(string id) 
        {
            User? user = await userRepository.GetUserByIdAsync(id);
            return mapper.Map<UserDTO>(user);
        }
    }
}