using LivePortfolio.Core.Models;

namespace LivePortfolio.Core.Services
{
    public interface IGameService
    {
        Task<GameDTO?> GetByIdAsync(int gameId);

        Task<GameDTO?> GetByIdWithReviewsAsync(int gameId);

        Task<IReadOnlyList<GameDTO>> GetAllAsync();

        Task<IReadOnlyList<GameDTO>> GetAllWithReviewsAsync();

        Task<double?> UpdateFinalGameScoreAsync(int gameId);
    }
}
