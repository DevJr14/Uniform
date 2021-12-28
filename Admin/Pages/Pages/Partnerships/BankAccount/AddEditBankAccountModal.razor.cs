using Clients.Infrastructure.Managers.Partnerships.BankAccount;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Partners;
using System;
using System.Threading.Tasks;

namespace Admin.Pages.Pages.Partnerships.BankAccount
{
    public partial class AddEditBankAccountModal
    {
        [Inject] private IBankAccountManager BankAccountManager { get; set; }
        [Parameter] public BankAccountRequest BankAccountRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        MudDatePicker _picker;

        public DateTime? _expiryDate { get; set; }

        protected override void OnInitialized()
        {
            if (BankAccountRequest.Id != Guid.Empty)
            {
                _expiryDate = BankAccountRequest.ExpiryDate;
            }
        }

        private async Task SaveAsync()
        {
            BankAccountRequest.ExpiryDate = (DateTime)_expiryDate;
            var response = await BankAccountManager.Save(BankAccountRequest);
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
