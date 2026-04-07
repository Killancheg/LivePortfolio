namespace LivePortfolio.Core.Models.DbEntities
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; } = null!;
    }
}
