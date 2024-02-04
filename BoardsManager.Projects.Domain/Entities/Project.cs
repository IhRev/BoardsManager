namespace BoardsManager.Projects.Domain.Entities
{
    public class Project
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string OwnerId { get; set; } = null!;
    }
}