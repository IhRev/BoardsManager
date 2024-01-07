using Microsoft.AspNetCore.Identity;

namespace BoardsManager.Users.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? ProjectId { get; set; }
    }
}