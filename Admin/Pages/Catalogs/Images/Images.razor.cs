using Admin.Pages.Catalogs.Products;
using Clients.Infrastructure.Managers.Catalogs.ProductImages;
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

namespace Admin.Pages.Catalogs.Images
{
    public partial class Images
    {
        [Inject] private IProductImageManager ProductImageManager { get; set; }
        [Parameter] public Guid ProductId { get; set; }
        public List<ProductImageResponse> ProductImageResponses { get; set; } = new();
        public ProductImageResponse ProductImage { get; set; } = new();
        public ProductImageRequest ProductImageRequest { get; set; } = new();

        private ClaimsPrincipal _currentUser;
        private bool _canCreateProductImages;
        private bool _canEditProductImages;
        private bool _canDeleteProductImages;
        private bool _canExportProductImages;
        private bool _canSearchProductImages;
        private bool _loaded;

        private string _searchString = "";

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateProductImages = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.ProductImages.Create)).Succeeded;
            _canEditProductImages = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.ProductImages.Edit)).Succeeded;
            _canDeleteProductImages = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.ProductImages.Delete)).Succeeded;
            _canExportProductImages = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.ProductImages.Export)).Succeeded;
            _canSearchProductImages = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.ProductImages.Search)).Succeeded;

            await GetProductImages();
        }

        private async Task GetProductImages()
        {
            var response = await ProductImageManager.GetForProduct(ProductId);
            _loaded = true;
            if (response.Succeeded)
            {
                ProductImageResponses = response.Data;
            }
        }

        private bool Search(ProductImageResponse productImage)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (productImage.Title?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (productImage.AltText?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
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
                var productImage = ProductImageResponses.FirstOrDefault(c => c.Id == id);
                if (productImage != null)
                {
                    parameters.Add(nameof(AddEditProductImage.ProductImageRequest), new ProductImageRequest
                    {
                        Id = productImage.Id,
                        Title = productImage.Title,
                        AltText = productImage.AltText,
                        ImageDataURL = productImage.ImageDataURL
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditProductImage>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetProductImages();
            }
        }

    }
}
