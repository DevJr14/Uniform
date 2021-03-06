using Clients.Infrastructure.Managers.Partnerships.BankAccount;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Partners;
using System;
using System.Threading.Tasks;

namespace Admin.Pages.Partnerships.Getting_Started
{
    public partial class AddBankingDetails
    {
        [Inject] private IBankAccountManager BankAccountManager { get; set; }
        [Parameter]
        public Guid PartnerId { get; set; }

        public DateTime? _expiryDate { get; set; }
        MudDatePicker _picker;
        public BankAccountRequest BankAccountRequest { get; set; } = new();

        private async Task SaveAsync()
        {
            BankAccountRequest.PartnerId = PartnerId;
            BankAccountRequest.ExpiryDate = (DateTime)_expiryDate;
            var response = await BankAccountManager.Save(BankAccountRequest);
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
            _navigationManager.NavigateTo($"/personal/dashboard");
        }
        private void Cancel()
        {
            _navigationManager.NavigateTo("/getting-started");
        }
    }
}
