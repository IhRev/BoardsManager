using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.DTO;

namespace BoardsManager.Users.Application.Services
{
    public class UserQueryService : IUserQueryService
    {
        public UserQueryService()
        {

        }
        public IAsyncEnumerable<UserDTO> GetUsersByProjectId(Guid projectId)
        {
            throw new NotImplementedException();
        }
    }
}