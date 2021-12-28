using Clients.Infrastructure.Managers.Partnerships.Address;
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

namespace Admin.Pages.Pages.Partnerships.Addresses
{
    public partial class Addresses
    {
        [Inject] private IAddressManager AddressManager { get; set; }
        [Parameter] public Guid PartnerId { get; set; }
        public List<AddressResponse> AddressResponses { get; set; } = new();
        public AddressResponse Address { get; set; } = new();
        public AddressRequest AddressRequest { get; set; } = new();

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
           
            await GetAddresses();
        }

        private async Task GetAddresses()
        {
            if (PartnerId != Guid.Empty)
            {
                var response = await AddressManager.GetForPartner(PartnerId);
                _loaded = true;
                if (response.Succeeded)
                {
                    AddressResponses = response.Data;
                }
            }
        }
        private bool Search(AddressResponse address)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (address.StreetName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (address.Suburb?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (address.City?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (address.Province?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (address.Country?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (address.PostalCode?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
        private async Task Delete(Guid id)
        {
            string deleteContent = "Are you sure you want to delete address?";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await AddressManager.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetAddresses();
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
                var address = AddressResponses.FirstOrDefault(c => c.Id == id);
                if (address != null)
                {
                    parameters.Add(nameof(AddEditAddressModal.AddressRequest), new AddressRequest
                    {
                        Id = address.Id,
                        PartnerId = address.PartnerId,
                        StreetName = address.StreetName,
                        Suburb = address.Suburb,
                        City = address.City,
                        Province = address.Province,
                        Country = address.Country,
                        PostalCode = address.PostalCode,
                        IsActive = address.IsActive
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditAddressModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetAddresses();
            }
        }
    }
}
