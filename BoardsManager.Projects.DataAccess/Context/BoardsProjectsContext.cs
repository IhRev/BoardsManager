using BoardsManager.Projects.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardsManager.Projects.DataAccess.Context
{
    public partial class BoardsProjectsContext : DbContext
    {
        public virtual DbSet<Project> Projects { get; set; }
    }
}