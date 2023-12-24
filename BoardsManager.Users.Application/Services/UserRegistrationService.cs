using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.DTO;

namespace BoardsManager.Users.Application.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        public UserRegistrationService()
        {
            
        }

        public Task<Guid> RegisterUser(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}