namespace BoardsManager.Core.DTO
{
    public class CardDTO
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;

        public string State { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Executor { get; set; } = null!;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public int ProjectId { get; set; }
    }
}