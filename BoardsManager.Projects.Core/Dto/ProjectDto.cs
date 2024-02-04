namespace BoardsManager.Projects.Core.Dto
{
    public class ProjectDto
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string OwnerId { get; set; } = null!;
    }
}