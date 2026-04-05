using LivePortfolio.Core.Services;
using LivePortfolio.Web.Mappers;
using LivePortfolio.Web.Models.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace LivePortfolio.Web.Components.Pages.Identity
{
    public partial class Register
    {
        [Inject]
        private IUserService UserService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private RegisterModel RegisterModel { get; set; } = new();

        private EditContext EditContext { get; set; } = default!;

        private ValidationMessageStore ValidationMessageStore { get; set; } = default!;

        protected override void OnInitialized()
        {
            EditContext = new EditContext(RegisterModel);
            ValidationMessageStore = new ValidationMessageStore(EditContext);
        }

        private async Task SubmitAsync()
        {
            ValidationMessageStore.Clear();

            var result = await UserService.RegisterAsync(RegisterModel.ToRegisterRequest());

            if (result.Succeeded)
            {
                NavigationManager.NavigateTo("/");
                return;
            }

            AddServiceErrors(result.Errors);
            EditContext.NotifyValidationStateChanged();
        }

        private void AddServiceErrors(IReadOnlyList<string> errors)
        {
            foreach (var error in errors)
            {
                ValidationMessageStore.Add(new FieldIdentifier(RegisterModel, string.Empty), error);
            }
        }
    }
}
