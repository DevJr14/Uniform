using Clients.Infrastructure.Managers.Catalogs.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Permission;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Tags
{
    public partial class Tags
    {
        [Inject] private ITagManager TagManager { get; set; }
        public List<TagResponse> TagResponses { get; set; } = new();
        public TagResponse Tag { get; set; } = new();
        public TagRequest TagRequest { get; set; } = new();

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

            await GetTags();
        }

        private async Task GetTags()
        {
            var response = await TagManager.GetForPartner();
            _loaded = true;
            if (response.Succeeded)
            {
                TagResponses = response.Data;
            }
        }

        private bool Search(TagResponse Tag)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (Tag.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task Delete(Guid id)
        {
            string deleteContent = "Are you sure you want to delete Tag?";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await TagManager.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetTags();
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
                var tag = TagResponses.FirstOrDefault(c => c.Id == id);
                if (tag != null)
                {
                    parameters.Add(nameof(AddEditTagModal.TagRequest), new TagRequest
                    {
                        Id = tag.Id,
                        PartnerId = tag.PartnerId,
                        Name = tag.Name
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditTagModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetTags();
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
