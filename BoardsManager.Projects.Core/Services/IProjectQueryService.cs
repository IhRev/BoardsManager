using BoardsManager.Projects.Core.Dto;

namespace BoardsManager.Projects.Core.Services
{
    public interface IProjectQueryService
    {
        Task<ProjectDto?> GetProjectByIdAsync(string id);

        Task<IEnumerable<ProjectDto>> GetProjectsByOwnerId(string ownerId);
    }
}