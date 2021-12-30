using Clients.Infrastructure.Managers.Catalogs.Products;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Responses.Catalogs;
using System;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Products
{
    public partial class ProductDetails
    {
        [Inject] private IProductManager ProductManager { get; set; }
        [Parameter]
        public Guid ProductId { get; set; }
        public ProductResponse ProductResponse { get; set; } = new();
        MudDatePicker _picker;
        protected override async Task OnInitializedAsync()
        {
            if (ProductId != Guid.Empty)
            {
                var response = await ProductManager.GetById(ProductId);
                if (response.Succeeded)
                {
                    ProductResponse = response.Data;
                }
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/pages/catalogs/products");
        }
    }
}
