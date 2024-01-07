using BoardsManager.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardsManager.Users.DataAccess.Context
{
    public class BoardsUsersContext : IdentityDbContext<User>
    {
        public BoardsUsersContext(DbContextOptions<BoardsUsersContext> options) : base(options)
        {
        }
    }
}