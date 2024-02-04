using BoardsManager.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardsManager.Users.DataAccess.Context
{
    public class BoardsUsersContext(DbContextOptions<BoardsUsersContext> options) : IdentityDbContext<User>(options)
    {
    }
}