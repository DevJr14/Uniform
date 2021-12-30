using Admin.Pages.Catalogs.Products;
using Clients.Infrastructure.Managers.Catalogs.Products;
using Clients.Infrastructure.Managers.Catalogs.Products;
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

namespace Admin.Pages.Catalogs.Products
{
    public partial class Products
    {
        [Inject] private IProductManager ProductManager { get; set; }
        public List<ProductResponse> ProductResponses { get; set; } = new();
        public ProductResponse Product { get; set; } = new();
        public ProductRequest ProductRequest { get; set; } = new();

        private ClaimsPrincipal _currentUser;
        private bool _canCreateProducts;
        private bool _canEditProducts;
        private bool _canDeleteProducts;
        private bool _canExportProducts;
        private bool _canSearchProducts;
        private bool _loaded;

        private string _searchString = "";

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateProducts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Products.Create)).Succeeded;
            _canEditProducts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Products.Edit)).Succeeded;
            _canDeleteProducts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Products.Delete)).Succeeded;
            _canExportProducts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Products.Export)).Succeeded;
            _canSearchProducts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Products.Search)).Succeeded;

            await GetProducts();
        }

        private async Task GetProducts()
        {
            var response = await ProductManager.GetForPartner();
            _loaded = true;
            if (response.Succeeded)
            {
                ProductResponses = response.Data;
            }
        }

        private bool Search(ProductResponse Product)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (Product.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (Product.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (Product.Barcode?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (Product.Brand.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (Product.Partner.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
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
                var response = await ProductManager.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetProducts();
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
                var product = ProductResponses.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    parameters.Add(nameof(AddEditProductModal.ProductRequest), new ProductRequest
                    {
                        Id = product.Id,
                        PartnerId = product.PartnerId,
                        Name = product.Name,
                        Description = product.Description,
                        Barcode = product.Barcode,
                        AllowReview = product.AllowReview,
                        BrandId = product.BrandId,
                        AvailableFrom = product.AvailableFrom,
                        AvailableTo = product.AvailableTo,
                        IsNew = product.IsNew,
                        IsActive = product.IsActive
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Large, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditProductModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetProducts();
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
