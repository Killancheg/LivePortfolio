using LivePortfolio.Core.Constants;
using LivePortfolio.Core.Models.Identity;
using LivePortfolio.Core.Services;
using LivePortfolio.Infrastructure.Identity;
using LivePortfolio.Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;

namespace LivePortfolio.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) 
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<RegisterResult> RegisterAsync(RegisterRequest request)
    {
        var newUser = request.ToApplicationUser();

        var createResult = await _userManager.CreateAsync(newUser, request.Password);
        if (!createResult.Succeeded)
        {
            return RegisterResult.Failure(createResult.Errors.Select(e => e.Description));
        }

        var roleResult = await _userManager.AddToRoleAsync(newUser, Roles.User);
        if (!roleResult.Succeeded)
        {
            return RegisterResult.Failure(roleResult.Errors.Select(e => e.Description));
        }

        return RegisterResult.Success();
    }

    public async Task<AppUser?> GetCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_signInManager.Context.User);
        if (user == null)
        {
            return null;
        }

        return user.ToAppUser();
    }
}
