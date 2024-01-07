using BoardsManager.Users.Domain.Entities;

namespace BoardsManager.Users.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);

        Task<bool> ChangePasswordAsync(User user, string currentPassword, string newPassword);
       
        Task<User?> GetUserByIdAsync(string id);
       
        IEnumerable<User> GetUsersByProjectId(string projectId);
       
        Task<bool> UpdateUserAsync(User user);
    }
}