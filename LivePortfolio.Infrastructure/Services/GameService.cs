using LivePortfolio.Core.Models;
using LivePortfolio.Core.Services;
using LivePortfolio.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LivePortfolio.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;

        public GameService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GameDTO> GetByIdAsync(int gameId)
        {
            var game = await _context.Games.Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.GameId == gameId);

            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} not found.");
            }

            return game.ToGameDTO();
        }

        public async Task<IReadOnlyList<GameDTO>> GetAllAsync()
        {
            var games = await _context.Games.Include(g => g.Reviews).ToListAsync();
            return [.. games.Select(g => g.ToGameDTO())];
        }
    }
}
