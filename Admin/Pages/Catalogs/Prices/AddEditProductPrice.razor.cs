using Clients.Infrastructure.Managers.Catalogs.ProductPrices;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using SharedR.Responses.Promotions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Prices
{
    public partial class AddEditProductPrice
    {
        [Inject] private IProductPriceManager ProductPriceManager { get; set; }
        [Parameter] public Guid ProductId { get; set; }
        public ProductPriceResponse ProductPriceResponse { get; set; } = new();
        public List<DiscountResponse> Discounts { get; set; } = new();
        public string DiscountName { get; set; }
        private bool _isLoaded;

        protected override async Task OnInitializedAsync()
        {
            var response = await ProductPriceManager.GetForProduct(ProductId);
            if (response.Succeeded)
            {
                ProductPriceResponse = response.Data;
                //DiscountName = ProductPriceResponse.Discount.Name;
            }
            _isLoaded = true;
        }

        private async Task SaveAsync()
        {
            var prodPriceRequest = new ProductPriceRequest()
            {
                Id = ProductPriceResponse.Id,
                Price = ProductPriceResponse.Price,
                OldPrice = ProductPriceResponse.OldPrice,
                Cost = ProductPriceResponse.Cost,
                DiscountId = ProductPriceResponse.DiscountId,
                ProductId = ProductId
            };
            var response = await ProductPriceManager.Save(prodPriceRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
            }
            else
            {
                _snackBar.Add(response.Messages[0], Severity.Error);
            }
        }
    }
}
