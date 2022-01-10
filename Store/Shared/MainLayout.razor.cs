using MudBlazor;

namespace Store.Shared
{
    public partial class MainLayout
    {
        private void Logout()
        {
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.LogoutConfirmation.ContentText), "Logout Confirmation" },
                {nameof(Dialogs.LogoutConfirmation.ButtonText), "Logout"},
                {nameof(Dialogs.LogoutConfirmation.Color), Color.Error},
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Dialogs.LogoutConfirmation>("Logout", parameters, options);
        }

        private void Login()
        {
            _navigationManager.NavigateTo("/pages/authentication/login");
        }
    }
}
