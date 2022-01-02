using Admin.Pages.Partnerships.Addresses;
using Clients.Infrastructure.Managers.Partnerships.Address;
using Clients.Infrastructure.Managers.Partnerships.BankAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Permission;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Pages.Partnerships.BankAccount
{
    public partial class BankAccounts
    {
        [Inject] private IBankAccountManager BankAccountManager { get; set; }
        [Parameter] public Guid PartnerId { get; set; }
        public List<BankAccountResponse> BankAccountResponses { get; set; } = new();
        public BankAccountResponse BankAccount { get; set; } = new();
        public BankAccountRequest BankAccountRequest { get; set; } = new();

        private ClaimsPrincipal _currentUser;
        private bool _canCreatePartners;
        private bool _canEditPartners;
        private bool _canDeletePartners;
        private bool _canExportPartners;
        private bool _canSearchPartners;
        private bool _loaded;

        private string _searchString = "";

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreatePartners = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Partners.Create)).Succeeded;
            _canEditPartners = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Partners.Edit)).Succeeded;
            _canDeletePartners = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Partners.Delete)).Succeeded;
            _canExportPartners = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Partners.Export)).Succeeded;
            _canSearchPartners = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Partners.Search)).Succeeded;

            await GetBankAccounts();
        }

        private async Task GetBankAccounts()
        {
            if (PartnerId != Guid.Empty)
            {
                var response = await BankAccountManager.GetForPartner(PartnerId);
                _loaded = true;
                if (response.Succeeded)
                {
                    BankAccountResponses = response.Data;
                }
            }
        }

        private bool Search(BankAccountResponse bank)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (bank.BankName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (bank.AccountType?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (bank.AccountNo?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task Delete(Guid id)
        {
            string deleteContent = "Are you sure you want to delete banking details?";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await BankAccountManager.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetBankAccounts();
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

        private async Task InvokeModal(Guid id)
        {
            var parameters = new DialogParameters();
            if (id != Guid.Empty)
            {
                var bank = BankAccountResponses.FirstOrDefault(c => c.Id == id);
                if (bank != null)
                {
                    parameters.Add(nameof(AddEditBankAccountModal.BankAccountRequest), new BankAccountRequest
                    {
                        Id = bank.Id,
                        PartnerId = bank.PartnerId,
                        BankName = bank.BankName,
                        AccountType = bank.AccountType,
                        AccountNo = bank.AccountNo,
                        ExpiryDate = bank.ExpiryDate,
                        CVV = bank.CVV,
                        IsActive = bank.IsActive
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditBankAccountModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetBankAccounts();
            }
        }
    }
}
