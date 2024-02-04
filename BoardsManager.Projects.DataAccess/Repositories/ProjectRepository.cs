using BoardsManager.Projects.DataAccess.Context;
using BoardsManager.Projects.Domain.Entities;
using BoardsManager.Projects.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BoardsManager.Projects.DataAccess.Repositories
{
    public class ProjectRepository(BoardsProjectsContext context) : IProjectRepository
    {
        public Task<Project?> GetProjectByIdAsync(string id) => context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Project>> GetProjectsByOwnerIdAsync(string ownerId) =>
            await context.Projects
            .Where(project => project.OwnerId == ownerId)
            .ToListAsync();

        public async Task<bool> AddProjectAsync(Project project)
        {
            await context.Projects.AddAsync(project);
            return await context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            context.Projects.Update(project);
            return await context.SaveChangesAsync() == 1;
        }
    }
}