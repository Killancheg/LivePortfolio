using LivePortfolio.Core.Models.Identity;
using LivePortfolio.Infrastructure.Identity;

namespace LivePortfolio.Infrastructure.Mappers
{
    public static class UserModelMapper
    {
        public static ApplicationUser ToApplicationUser(this RegisterRequest request)
        {
            return new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };
        }
    }
}
