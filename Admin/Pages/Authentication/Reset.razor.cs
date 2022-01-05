using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using SharedR.Requests.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Pages.Authentication
{
    public partial class Reset
    {
        private readonly ResetPasswordRequest ResetPasswordRequest = new();

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        protected override void OnInitialized()
        {
            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("Token", out var param))
            {
                var queryToken = param.First();
                ResetPasswordRequest.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(queryToken));
            }
        }

        private async Task SubmitAsync()
        {
            if (!string.IsNullOrEmpty(ResetPasswordRequest.Token))
            {
                var result = await _userManager.ResetPasswordAsync(ResetPasswordRequest);
                if (result.Succeeded)
                {
                    _snackBar.Add(result.Messages[0], Severity.Success);
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    foreach (var message in result.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
            else
            {
                _snackBar.Add("Token Not Found!", Severity.Error);
            }
        }        

        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
    }
}
