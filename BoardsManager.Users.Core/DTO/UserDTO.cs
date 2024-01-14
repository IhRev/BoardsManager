namespace BoardsManager.Users.Core.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = string.Empty;
    }
}