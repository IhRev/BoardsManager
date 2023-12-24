using BoardsManager.Users.Core.DTO;

namespace BoardsManager.Users.Core.Abstractions
{
    public interface IUserQueryService
    {
        IAsyncEnumerable<UserDTO> GetUsersByProjectId(Guid projectId);
    }
}