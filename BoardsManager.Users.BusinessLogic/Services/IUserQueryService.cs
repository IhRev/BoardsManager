namespace BoardsManager.Users.BusinessLogic.Services
{
    public interface IUserQueryService
    {
        void GetUsersByProject(Guid projectId);
    }
}