using Clients.Infrastructure.Managers.Partnerships.Address;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Partners;
using System.Threading.Tasks;

namespace Admin.Pages.Pages.Partnerships.Addresses
{
    public partial class AddEditAddressModal
    {
        [Inject] private IAddressManager AddressManager { get; set; }
        [Parameter] public AddressRequest AddressRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private async Task SaveAsync()
        {
            var response = await AddressManager.Save(AddressRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
