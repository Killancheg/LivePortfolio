using LivePortfolio.Core.Models.Identity;

namespace LivePortfolio.Core.Services
{
    public interface IUserService
    {
        Task<RegisterResult> RegisterAsync(RegisterRequest request);

        Task<AppUser?> GetCurrentUserAsync();
    }
}
