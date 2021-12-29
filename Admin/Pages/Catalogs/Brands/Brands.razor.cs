using Admin.Pages.Catalogs.Categories;
using Clients.Infrastructure.Managers.Catalogs.Brands;
using Clients.Infrastructure.Managers.Catalogs.Categories;
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

namespace Admin.Pages.Catalogs.Brands
{
    public partial class Brands
    {
        [Inject] private IBrandManager BrandManager { get; set; }
        public List<BrandResponse> BrandResponses { get; set; } = new();
        public BrandResponse Brand { get; set; } = new();
        public BrandRequest BrandRequest { get; set; } = new();

        private ClaimsPrincipal _currentUser;
        private bool _canCreateBrands;
        private bool _canEditBrands;
        private bool _canDeleteBrands;
        private bool _canExportBrands;
        private bool _canSearchBrands;
        private bool _loaded;

        private string _searchString = "";

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Create)).Succeeded;
            _canEditBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Edit)).Succeeded;
            _canDeleteBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Delete)).Succeeded;
            _canExportBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Export)).Succeeded;
            _canSearchBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Search)).Succeeded;

            await GetBrands();
        }

        private async Task GetBrands()
        {
            var response = await BrandManager.GetForPartner();
            _loaded = true;
            if (response.Succeeded)
            {
                BrandResponses = response.Data;
            }
        }

        private bool Search(BrandResponse Brand)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (Brand.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (Brand.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task Delete(Guid id)
        {
            string deleteContent = "Are you sure you want to delete brand?";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await BrandManager.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetBrands();
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
                var brand = BrandResponses.FirstOrDefault(c => c.Id == id);
                if (brand != null)
                {
                    parameters.Add(nameof(AddEditBrandModal.BrandRequest), new BrandRequest
                    {
                        Id = brand.Id,
                        PartnerId = brand.PartnerId,
                        Name = brand.Name,
                        Description = brand.Description
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditBrandModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetBrands();
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
