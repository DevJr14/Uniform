using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using SharedR.Requests.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Pages.Authentication
{
    public partial class Login
    {
        private TokenRequest _tokenModel = new();

        //protected override async Task OnInitializedAsync()
        //{
        //    var state = await _stateProvider.GetAuthenticationStateAsync();
        //    if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
        //    {
        //        _navigationManager.NavigateTo("/");
        //    }
        //}
        private async Task SubmitAsync()
        {
            var result = await _authenticationManager.Login(_tokenModel);
            if (result.Succeeded)
            {
                _navigationManager.NavigateTo("/");
                _snackBar.Add(string.Format("Welcome {0}", _tokenModel.Email), Severity.Success);
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        void TogglePasswordVisibility()
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

        private void FillCredentials()
        {
            _tokenModel.Email = "junior@rus.com";
            _tokenModel.Password = "Junior@1234";
        }
    }
}
