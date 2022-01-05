using Admin.Pages.Partnerships.Addresses;
using Clients.Infrastructure.Managers.Partnerships.Address;
using Clients.Infrastructure.Managers.Partnerships.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Constants.Permission;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Pages.Partnerships.Contacts
{
    public partial class Contacts
    {
        [Inject] private IContactManager ContactManager { get; set; }
        [Parameter] public Guid PartnerId { get; set; }
        public ContactRequest ContactRequest { get; set; } = new();
        public List<ContactResponse> ContactResponses { get; set; } = new();
        public ContactResponse Contact { get; set; } = new();
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

            await GetContacts();
        }

        private async Task GetContacts()
        {
            if (PartnerId != Guid.Empty)
            {
                var response = await ContactManager.GetForPartner(PartnerId);
                _loaded = true;
                if (response.Succeeded)
                {
                    ContactResponses = response.Data;
                }
            }
        }

        private bool Search(ContactResponse contact)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (contact.Title?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (contact.CellphoneNo?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (contact.TelephoneNo?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (contact.Email?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task Delete(Guid id)
        {
            string deleteContent = "Are you sure you want to delete contact?";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await ContactManager.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetContacts();
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
                var contact = ContactResponses.FirstOrDefault(c => c.Id == id);
                if (contact != null)
                {
                    parameters.Add(nameof(AddEditContactModal.ContactRequest), new ContactRequest
                    {
                        Id = contact.Id,
                        PartnerId = contact.PartnerId,
                        Title = contact.Title,
                        CellphoneNo = contact.CellphoneNo,
                        TelephoneNo = contact.TelephoneNo,
                        Email = contact.Email,
                        IsActive = contact.IsActive
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditContactModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetContacts();
            }
        }
    }
}
