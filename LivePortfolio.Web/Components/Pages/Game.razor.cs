using LivePortfolio.Core.Models;
using LivePortfolio.Core.Services;
using Microsoft.AspNetCore.Components;

namespace LivePortfolio.Web.Components.Pages
{
    public partial class Game
    {
        [Inject]
        private IGameService GameService { get; set; } = default!;

        private GameDTO? CurrentGame { get; set; }

        private bool _isLoading = true;

        [Parameter]
        public int GameId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentGame = await GameService.GetByIdWithReviewsAsync(GameId);

            _isLoading = false;
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
