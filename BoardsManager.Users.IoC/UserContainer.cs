using BoardsManager.Users.Application.Mappings;
using BoardsManager.Users.Application.Services;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.DataAccess.Repositories;
using BoardsManager.Users.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BoardsManager.Users.DataAccess.Context;
using BoardsManager.Users.Domain.Entities;

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

        public static void RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BoardsUsersContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<BoardsUsersContext>()
               .AddDefaultTokenProviders();
        }
    }
}