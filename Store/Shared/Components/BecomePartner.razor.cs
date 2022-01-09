using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Shared.Components
{
    public partial class BecomePartner
    {
        public string RedirectUrl { get; set; } = "";
        protected override async Task OnInitializedAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
            {
                RedirectUrl = "/pages/authentication/login";
            }
            else
            {
                RedirectUrl = "/partnership/add-partner";
            }
        }

        private void RedirectTo()
        {
            _navigationManager.NavigateTo(RedirectUrl);
        }
    }
}
