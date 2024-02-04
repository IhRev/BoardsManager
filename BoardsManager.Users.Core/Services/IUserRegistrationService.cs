using BoardsManager.Users.Core.Dto;

namespace BoardsManager.Users.Core.Abstractions
{
    public interface IUserRegistrationService
    {
        Task<bool> AddUserToProjectAsync(string projectId, string userId);

        Task<bool> ChangeUserPasswordAsync(string userId, string currentPassword, string newPassword);
        
        Task<bool> CreateUserAsync(UserDto userDTO);
       
        Task<bool> UpdateUserAsync(UserDto userDTO);
    }
}