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

    public UserService(UserManager<ApplicationUser> userManager) 
    {
        _userManager = userManager;
    }

    public async Task<RegisterResult> RegisterAsync(RegisterRequest request)
    {
        var newUser = request.ToApplicationUser();

        var registerResult = await _userManager.CreateAsync(newUser, request.Password);

        if (registerResult.Succeeded) await _userManager.AddToRoleAsync(newUser, Roles.User);

        return GetRegisterResult(registerResult);
    }

    private RegisterResult GetRegisterResult(IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return RegisterResult.Success();
        }

        return RegisterResult.Failure(identityResult.Errors.Select(e => e.Description));
    }
}
