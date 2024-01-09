using BoardsManager.Users.Application.Mappings;
using BoardsManager.Users.Application.Services;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.DataAccess.Repositories;
using BoardsManager.Users.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BoardsManager.Users.IoC
{
    public static class UserContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IUserQueryService, UserQueryService>();
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();

            //Repositories
            services.AddScoped<IUserRepository, UsersRepository>();

            //mapper
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}