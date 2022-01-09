using Clients.Infrastructure.Managers.Partnerships.Contact;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Pages.Partnerships
{
    public partial class AddContact
    {
        [Inject] private IContactManager ContactManager { get; set; }
        [Parameter]
        public Guid PartnerId { get; set; }
        public ContactRequest ContactRequest { get; set; } = new();

        private async Task SaveAsync()
        {
            ContactRequest.PartnerId = PartnerId;
            var response = await ContactManager.Save(ContactRequest);
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
            _navigationManager.NavigateTo($"/partnership/add-banking-details/{PartnerId}");
        }
        private void Cancel()
        {
            _navigationManager.NavigateTo("/getting-started");
        }
    }
}
