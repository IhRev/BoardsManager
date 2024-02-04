using BoardsManager.Projects.Application.Mappings;
using BoardsManager.Projects.Application.Services;
using BoardsManager.Projects.Core.Services;
using BoardsManager.Projects.DataAccess.Context;
using BoardsManager.Projects.DataAccess.Repositories;
using BoardsManager.Projects.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoardsManager.Projects.IoC
{
    public static class ProjectContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IProjectQueryService, ProjectQueryService>();
            services.AddScoped<IProjectManagementService, ProjectManagementService>();

            //Repositories
            services.AddScoped<IProjectRepository, ProjectRepository>();

            //mapper
            services.AddAutoMapper(typeof(ProjectProfile));
        }

        public static void RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BoardsProjectsContext>(options => options.UseSqlServer(connectionString));
        }
    }
}