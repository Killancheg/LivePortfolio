using LivePortfolio.Core.Services;
using LivePortfolio.Web.Mappers;
using LivePortfolio.Web.Models.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace LivePortfolio.Web.Components.Pages.Identity
{
    public partial class LoginPanel
    {
        [Parameter]
        public string? ErrorCode { get; set; }

        [Parameter]
        public string ReturnUrl { get; set; } = "/";

        private string? ErrorMessage => ErrorCode switch
        {
            "InvalidCredentials" => "Invalid email or password.",
            "LockedOut" => "Your account is locked out.",
            "NotAllowed" => "Login is not allowed for this account.",
            "RequiresTwoFactor" => "This account requires two-factor authentication.",
            _ => null
        };
    }
}
