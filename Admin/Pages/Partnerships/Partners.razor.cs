using Clients.Infrastructure.Managers.Partnerships.Partner;
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

namespace Admin.Pages.Partnerships
{
    public partial class Partners
    {
        [Inject] private IPartnerManager PartnerManger { get; set; }

        public List<PartnerResponse> _partners { get; set; } = new();
        public PartnerResponse _partner { get; set; } = new();
        public PartnerRequest _partnerRequest { get; set; } = new();

        private ClaimsPrincipal _currentUser;
        private bool _canCreatePartners;
        private bool _canEditPartners;
        private bool _canDeletePartners;
        private bool _canExportPartners;
        private bool _canSearchPartners;
        private bool _canActivatePartners;
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
            _canActivatePartners = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Partners.Activate)).Succeeded;

            await GetPartners();
            _loaded = true;
        }

        private async Task GetPartners()
        {
            var response = await PartnerManger.GetAll();
            if (response.Succeeded)
            {
                _partners = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(PartnerResponse partner)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (partner.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (partner.RegistrationNo?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (partner.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task InvokeModal(Guid id)
        {
            var parameters = new DialogParameters();
            if (id != Guid.Empty)
            {
                var partner = _partners.FirstOrDefault(c => c.Id == id);
                if (partner != null)
                {
                    parameters.Add(nameof(AddEditPartnerModal.PartnerRequest), new PartnerRequest
                    {
                        Id = partner.Id,
                        Name = partner.Name,
                        Description = partner.Description,
                        IsVerified = partner.IsVerified,
                        Type = partner.Type,
                        RegistrationDate = partner.RegistrationDate,
                        RegistrationNo = partner.RegistrationNo,
                        TaxNo = partner.TaxNo,
                        UserId = partner.UserId
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditPartnerModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetPartners();
            }
        }

        private async Task Activator(Guid id)
        {
            var parameters = new DialogParameters();
            var partner = _partners.FirstOrDefault(p => p.Id == id);
            if (partner.IsVerified)
            {
                parameters.Add(nameof(ActivateDeActivateModal.ContentText), "Are you sure you want to de-activate partner?");
                parameters.Add(nameof(ActivateDeActivateModal.Title), "De-Activate");
            }
            else
            {
                parameters.Add(nameof(ActivateDeActivateModal.ContentText), "Are you sure you want to activate partner?");
                parameters.Add(nameof(ActivateDeActivateModal.Title), "Activate");
            }
            
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<ActivateDeActivateModal>("", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await PartnerManger.ActivateDeActiate(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetPartners();
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

        private async Task Delete(Guid id)
        {
            string deleteContent = "Are you sure you want to delete partner?";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await PartnerManger.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetPartners();
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

        private void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
