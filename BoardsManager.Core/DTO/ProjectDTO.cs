namespace BoardsManager.Core.DTO
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int AuthorId { get; set; }
    }
}