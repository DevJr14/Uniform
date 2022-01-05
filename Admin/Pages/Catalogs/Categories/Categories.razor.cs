using Clients.Infrastructure.Managers.Catalogs.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Constants.Permission;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Categories
{
    public partial class Categories
    {
        [Inject] private ICategoryManager CategoryManager { get; set; }
        public List<CategoryResponse> CategoryResponses { get; set; } = new();
        public CategoryResponse Category { get; set; } = new();
        public CategoryRequest CategoryRequest { get; set; } = new();

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

            await GetCategories();
        }

        private async Task GetCategories()
        {
            var response = await CategoryManager.GetForPartner();
            _loaded = true;
            if (response.Succeeded)
            {
                CategoryResponses = response.Data;
            }
        }

        private bool Search(CategoryResponse category)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (category.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (category.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task Delete(Guid id)
        {
            string deleteContent = "Are you sure you want to delete category?";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await CategoryManager.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetCategories();
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
                var category = CategoryResponses.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    parameters.Add(nameof(AddEditCategoryModal.CategoryRequest), new CategoryRequest
                    {
                        Id = category.Id,
                        PartnerId = category.PartnerId,
                        Name = category.Name,
                        Description = category.Description,
                        IsActive = category.IsActive
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetCategories();
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
