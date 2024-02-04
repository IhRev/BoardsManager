using BoardsManager.Projects.Domain.Entities;

namespace BoardsManager.Projects.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<bool> AddProjectAsync(Project project);

        Task<Project?> GetProjectByIdAsync(string id);

        Task<IEnumerable<Project>> GetProjectsByOwnerIdAsync(string ownerId);

        Task<bool> UpdateProjectAsync(Project project);
    }
}