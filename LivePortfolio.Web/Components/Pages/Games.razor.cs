using LivePortfolio.Core.Models;
using LivePortfolio.Core.Services;
using Microsoft.AspNetCore.Components;

namespace LivePortfolio.Web.Components.Pages
{
    public partial class Games
    {
        [Inject]
        private IGameService GameService { get; set; } = default!;

        private IReadOnlyList<GameDTO> GamesList { get; set; } = [];

        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            GamesList = await GameService.GetAllAsync();

            _isLoading = false;
        }

        private string GetShortDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return "No description.";
            }

            var words = description
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (words.Length <= 50)
            {
                return description;
            }

            return string.Join(' ', words.Take(50)) + "...";
        }

        private string GetCoverImageUrl(GameDTO game)
        {
            if (!string.IsNullOrWhiteSpace(game.CoverImageUrl))
            {
                return game.CoverImageUrl;
            }

            return "/images/placeholder-cover.png";
        }
    }
}
