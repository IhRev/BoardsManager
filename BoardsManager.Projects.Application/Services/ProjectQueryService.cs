using AutoMapper;
using BoardsManager.Projects.Core.Dto;
using BoardsManager.Projects.Core.Services;
using BoardsManager.Projects.Domain.Entities;
using BoardsManager.Projects.Domain.Repositories;

namespace BoardsManager.Projects.Application.Services
{
    public class ProjectQueryService(IProjectRepository projectRepository, IMapper mapper) : IProjectQueryService
    {
        public async Task<IEnumerable<ProjectDto>> GetProjectsByOwnerId(string ownerId)
        {
            IEnumerable<Project> projects = await projectRepository.GetProjectsByOwnerIdAsync(ownerId);
            return mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(string id)
        {
            Project? project = await projectRepository.GetProjectByIdAsync(id);
            return mapper.Map<ProjectDto>(project);
        }
    }
}