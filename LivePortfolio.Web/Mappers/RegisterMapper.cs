using LivePortfolio.Core.Models.Identity;
using LivePortfolio.Web.Models.Identity;

namespace LivePortfolio.Web.Mappers
{
    public static class RegisterMapper
    {
        public static RegisterRequest ToRegisterRequest(this RegisterModel model) =>
            new(model.UserName, model.Email, model.Password);
    }
}
