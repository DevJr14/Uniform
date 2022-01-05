using MudBlazor;
using SharedR.Requests.Identity;
using System.Threading.Tasks;

namespace Admin.Pages.Authentication
{
    public partial class Forgot
    {
        private readonly ForgotPasswordRequest ForgotPasswordRequest = new();

        private async Task SubmitAsync()
        {
            var response = await _userManager.ForgotPasswordAsync(ForgotPasswordRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}
