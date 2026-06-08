using LivePortfolio.Core.Models;
using LivePortfolio.Core.Services;
using LivePortfolio.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace LivePortfolio.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GameService> _logger;

        public GameService(AppDbContext context, ILogger<GameService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GameDTO?> GetByIdAsync(int gameId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);

            if (game == null)
            {
                return null;
            }

            return game.ToGameDTO();
        }

        public async Task<GameDTO?> GetByIdWithReviewsAsync(int gameId)
        {
            var game = await _context.Games.Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.GameId == gameId);

            if (game == null)
            {
                return null;
            }

            return game.ToGameDTO();
        }

        [DoesNotReturn]
        private void ThrowGameNotFoundError(int gameId)
        {
            var errorMessage = $"Game with ID {gameId} not found.";
            _logger.LogWarning(errorMessage);
            throw new KeyNotFoundException(errorMessage);
        }

        public async Task<IReadOnlyList<GameDTO>> GetAllAsync()
        {
            var games = await _context.Games.Where(g => g.IsActive && g.IsApproved).ToListAsync();

            return [.. games.Select(g => g.ToGameDTO())];
        }

        public async Task<IReadOnlyList<GameDTO>> GetAllWithReviewsAsync()
        {
            var games = await _context.Games
                .Where(g => g.IsActive && g.IsApproved)
                .Include(g => g.Reviews)
                .ToListAsync();

            return [.. games.Select(g => g.ToGameDTO())];
        }

        public async Task<double?> UpdateFinalGameScoreAsync(int gameId)
        {
            var game = await _context.Games.Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.GameId == gameId);

            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} not found.");
            }

            throw new NotImplementedException();
        }

        private ICollection<ReviewScoreInput> GetReviewScoreCollection()
        {
            throw new NotImplementedException();
        }
    }
}
