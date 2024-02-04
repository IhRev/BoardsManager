using AutoMapper;
using BoardsManager.Projects.Core.Dto;
using BoardsManager.Projects.Core.Services;
using BoardsManager.Projects.Domain.Entities;
using BoardsManager.Projects.Domain.Repositories;

namespace BoardsManager.Projects.Application.Services
{
    public class ProjectManagementService(IProjectRepository projectRepository, IMapper mapper) 
        : IProjectManagementService
    {
        public Task<bool> CreateProjectAsync(ProjectDto projectDto)
        {
            Project project = mapper.Map<Project>(projectDto);
            return projectRepository.AddProjectAsync(project);
        }

        public async Task<bool> UpdateProject(ProjectDto projectDto)
        {
            Project? project = await GetProject(projectDto.Id);
            if (project == null)
            {
                return false;
            }
            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            return await projectRepository.UpdateProjectAsync(project);
        }

        private Task<Project?> GetProject(string projectId) => projectRepository.GetProjectByIdAsync(projectId);
    }
}