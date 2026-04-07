namespace LivePortfolio.Core.Models.DbEntities
{
    public class Game
    {
        public Game()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public int GameId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? CoverImageUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatorId { get; set; } = string.Empty;

        public bool IsApproved { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Review> Reviews { get; set; } = [];
    }
}
