using BoardsManager.Users.Domain.Entities;
using BoardsManager.Users.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BoardsManager.Users.DataAccess.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;

        public UsersRepository(UserManager<User> userManager) => this.userManager = userManager;

        public async Task<bool> AddUserAsync(User user)
        {
            IdentityResult result = await userManager.CreateAsync(user);
            return result.Succeeded;
        }

        public Task<User?> GetUserByIdAsync(string id) => userManager.FindByIdAsync(id);

        public IEnumerable<User> GetUsersByProjectId(string projectId) => userManager.Users.Where(_ => _.ProjectId == projectId);

        public async Task<bool> UpdateUserAsync(User user)
        {
            IdentityResult result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            IdentityResult result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }
    }
}