using BoardsManager.Users.Core.DTO;

namespace BoardsManager.Users.Core.Abstractions
{
    public interface IUserRegistrationService
    {
        Task<Guid> RegisterUser(UserDTO userDTO);
    }
}