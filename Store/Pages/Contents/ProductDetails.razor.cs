using Clients.Infrastructure.Managers.Catalogs.Products;
using Microsoft.AspNetCore.Components;
using SharedR.Responses.Catalogs;
using System;
using System.Threading.Tasks;

namespace Store.Pages.Contents
{
    public partial class ProductDetails
    {
        [Inject] private IProductManager ProductManager { get; set; }
        public ProductResponse ProductResponse { get; set; } = new();
        [Parameter] public Guid ProductId { get; set; }
        private bool _Loaded;
        protected override async Task OnInitializedAsync()
        {
            await LoadProductDetails();
        }

        private async Task LoadProductDetails()
        {
            var response = await ProductManager.GetById(ProductId);
            _Loaded = true;
            if (response.Succeeded)
            {
                ProductResponse = response.Data;
            }
        }
        private void Cancel()
        {
            _navigationManager.NavigateTo("/shopping/product-listing");
        }
    }
}
