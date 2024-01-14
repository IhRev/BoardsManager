using BoardsManager.Users.Core.DTO;

namespace BoardsManager.Users.Core.Abstractions
{
    public interface IUserRegistrationService
    {
        Task<bool> AddUserToProjectAsync(string projectId, string userId);

        Task<bool> ChangeUserPasswordAsync(string userId, string currentPassword, string newPassword);
        
        Task<bool> CreateUserAsync(UserDTO userDTO);
       
        Task<bool> UpdateUserAsync(UserDTO userDTO);
    }
}