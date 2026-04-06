using LivePortfolio.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LivePortfolio.Web
{
    public static class EndpointRegistrations
    {
        public static void MapIdentity(this WebApplication app)
        {
            app.MapPost("/account/login", async (
                HttpContext httpContext,
                [FromForm] string email,
                [FromForm] string password,
                [FromForm] string? returnUrl,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager) =>
                {
                    var user = await userManager.FindByEmailAsync(email);

                    if (user is null)
                    {
                        return Results.LocalRedirect($"/?loginError=InvalidCredentials");
                    }

                    var result = await signInManager.PasswordSignInAsync(
                        user,
                        password,
                        isPersistent: false,
                        lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        return Results.LocalRedirect(string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);
                    }

                    if (result.IsLockedOut)
                    {
                        return Results.LocalRedirect("/?loginError=LockedOut");
                    }

                    if (result.IsNotAllowed)
                    {
                        return Results.LocalRedirect("/?loginError=NotAllowed");
                    }

                    if (result.RequiresTwoFactor)
                    {
                        return Results.LocalRedirect("/?loginError=RequiresTwoFactor");
                    }

                    return Results.LocalRedirect("/?loginError=InvalidCredentials");
                }
            );

            app.MapPost("/account/logout", async (
                SignInManager<ApplicationUser> signInManager) =>
                {
                    await signInManager.SignOutAsync();
                    return Results.LocalRedirect("/");
                }
            );
        }
    }
}
