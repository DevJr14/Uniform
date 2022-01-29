using Clients.Infrastructure.Managers.Catalogs.Products;
using Microsoft.AspNetCore.Components;
using SharedR.Responses.Catalogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Pages.Contents
{
    public partial class ProductList
    {
        [Inject] private IProductManager ProductManager { get; set; }
        public List<ProductResponse> ProductResponses { get; set; } = new();
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            await LoadProductListing();
        }

        private async Task LoadProductListing()
        {
            var response = await ProductManager.GetAll();
            _loaded = true;
            if (response.Succeeded)
            {
                ProductResponses = response.Data;
            }
        }
    }
}
