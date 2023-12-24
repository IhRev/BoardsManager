using BoardsManager.Users.Application.Services;
using BoardsManager.Users.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BoardsManager.Users.IoC
{
    public static class UsersContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IUserQueryService, UserQueryService>();
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();

            //Repositories

        }
    }
}