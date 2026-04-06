using LivePortfolio.Core.Models.Identity;
using LivePortfolio.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace LivePortfolio.Web.Components.Layout
{
    public partial class MainLayout
    {
        [Inject]
        public IUserService UserService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private AppUser? CurrentUser { get; set; }
        private bool IsLoginPanelOpen { get; set; }
        private string? LoginErrorCode { get; set; }
        private string CurrentRelativeUrl { get; set; } = "/";

        protected override async Task OnInitializedAsync()
        {
            CurrentUser = await UserService.GetCurrentUserAsync();

            var uri = new Uri(NavigationManager.Uri);
            CurrentRelativeUrl = uri.PathAndQuery;

            var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);

            if (query.TryGetValue("loginError", out var loginError))
            {
                LoginErrorCode = loginError.ToString();
                IsLoginPanelOpen = true;
            }
        }

        private void ToggleLoginPanel()
        {
            IsLoginPanelOpen = !IsLoginPanelOpen;
        }
    }
}
