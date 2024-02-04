using BoardsManager.Projects.Core.Dto;

namespace BoardsManager.Projects.Core.Services
{
    public interface IProjectManagementService
    {
        Task<bool> CreateProjectAsync(ProjectDto projectDto);

        Task<bool> UpdateProject(ProjectDto projectDto);
    }
}