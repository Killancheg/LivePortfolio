namespace LivePortfolio.Core.Models
{
    public sealed record ReviewDTO(
        int ReviewId
    );

    public sealed record GameDTO(
        int GameId,
        string Title,
        string? Description,
        string? CoverImageUrl,
        DateTime ReleaseDate,
        DateTime CreatedAt,
        string CreatorId,
        bool IsApproved,
        bool IsActive,
        IReadOnlyList<ReviewDTO> Reviews
    );
}
