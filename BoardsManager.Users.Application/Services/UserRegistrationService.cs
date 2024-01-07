using AutoMapper;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.DTO;
using BoardsManager.Users.Domain.Entities;
using BoardsManager.Users.Domain.Repositories;

namespace BoardsManager.Users.Application.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserRegistrationService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AssignUserToProjectAsync(string projectId, string userId)
        {
            User? user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.ProjectId = projectId;
            return await userRepository.UpdateUserAsync(user);
        }

        public Task<bool> CreateUserAsync(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            return userRepository.AddUserAsync(user);
        }

        public async Task<bool> ChangeUserPasswordAsync(string userId, string currentPassword, string newPassword)
        {
            User? user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            return await userRepository.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<bool> UpdateUserAsync(UserDTO userDTO)
        {
            User? user = await userRepository.GetUserByIdAsync(userDTO.Id);
            if (user == null)
            {
                return false;
            }
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            return await userRepository.UpdateUserAsync(user);
        }
    }
}