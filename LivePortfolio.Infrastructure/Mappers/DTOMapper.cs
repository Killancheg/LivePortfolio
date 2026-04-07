using LivePortfolio.Core.Models;
using LivePortfolio.Core.Models.DbEntities;

namespace LivePortfolio.Infrastructure.Mappers
{
    public static class DTOMapper
    {
        public static GameDTO ToGameDTO(this Game game)
        {
            return new GameDTO(

                game.GameId,
                game.Title,
                game.Description,
                game.CoverImageUrl,
                game.ReleaseDate,
                game.CreatedAt,
                game.CreatorId,
                game.IsApproved,
                game.IsActive,
                [.. game.Reviews.Select(r => r.ToReviewDTO())]
            );
        }

        public static ReviewDTO ToReviewDTO(this Review review)
        {
            return new ReviewDTO(review.ReviewId);
        }
    }
}
