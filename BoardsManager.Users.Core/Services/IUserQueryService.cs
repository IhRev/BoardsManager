using BoardsManager.Users.Core.Dto;

namespace BoardsManager.Users.Core.Abstractions
{
    public interface IUserQueryService
    {
        Task<UserDto?> GetUserByIdAsync(string id);

        IEnumerable<UserDto> GetUsersByProjectId(string projectId);
    }
}