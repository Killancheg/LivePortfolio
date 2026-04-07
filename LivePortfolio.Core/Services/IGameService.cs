using LivePortfolio.Core.Models;

namespace LivePortfolio.Core.Services
{
    public interface IGameService
    {
        Task<GameDTO> GetByIdAsync(int gameId);

        Task<IReadOnlyList<GameDTO>> GetAllAsync();
    }
}
