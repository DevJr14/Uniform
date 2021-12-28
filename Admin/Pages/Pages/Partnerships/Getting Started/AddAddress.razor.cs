using Clients.Infrastructure.Managers.Partnerships.Address;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Partners;
using System;
using System.Threading.Tasks;

namespace Admin.Pages.Pages.Partnerships.Getting_Started
{
    public partial class AddAddress
    {
        [Inject] private IAddressManager AddressManager { get; set; }
        [Parameter]
        public Guid PartnerId { get; set; }
        public AddressRequest AddressRequest { get; set; } = new();

        private async Task SaveAsync()
        {
            AddressRequest.PartnerId = PartnerId;
            var response = await AddressManager.Save(AddressRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                Continue();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private void Continue()
        {
            _navigationManager.NavigateTo($"/partnership/add-contact/{PartnerId}");
        }
        private void Cancel()
        {
            _navigationManager.NavigateTo("/getting-started");
        }
    }
}
