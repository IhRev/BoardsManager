using AutoMapper;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.Dto;
using BoardsManager.Users.Domain.Entities;
using BoardsManager.Users.Domain.Repositories;

namespace BoardsManager.Users.Application.Services
{
    public class UserRegistrationService(IUserRepository userRepository, IMapper mapper) 
        : IUserRegistrationService
    {
        public async Task<bool> AddUserToProjectAsync(string projectId, string userId)
        {
            User? user = await GetUser(userId);
            if (user == null)
            {
                return false;
            }
            user.ProjectId = projectId;
            return await userRepository.UpdateUserAsync(user);
        }

        public Task<bool> CreateUserAsync(UserDto userDto)
        {
            User user = mapper.Map<User>(userDto);
            return userRepository.AddUserAsync(user);
        }

        public async Task<bool> ChangeUserPasswordAsync(string userId, string currentPassword, string newPassword)
        {
            User? user = await GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return await userRepository.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            User? user = await GetUser(userDto.Id);
            if (user == null)
            {
                return false;
            }
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            return await userRepository.UpdateUserAsync(user);
        }

        private Task<User?> GetUser(string userId) => userRepository.GetUserByIdAsync(userId);
    }
}