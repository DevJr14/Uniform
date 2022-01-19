using Clients.Infrastructure.Managers.Catalogs.ProductPrices;
using Clients.Infrastructure.Managers.Promotion.Discount;
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
        [Inject] private IDiscountManager DiscountManager { get; set; }
        [Parameter] public Guid ProductId { get; set; }
        public ProductPriceResponse ProductPriceResponse { get; set; } = new();
        public List<DiscountResponse> Discounts { get; set; } = new();
        public Guid _discountId;
        private bool _isLoaded;

        protected override async Task OnInitializedAsync()
        {
            await LoadPrice();
            await LoadDiscounts();
            _isLoaded = true;
        }

        private async Task LoadPrice()
        {
            var response = await ProductPriceManager.GetForProduct(ProductId);
            if (response.Succeeded)
            {
                ProductPriceResponse = response.Data;
                _discountId = (Guid)ProductPriceResponse.DiscountId;               
            }
        }

        private async Task LoadDiscounts()
        {
            var response = await DiscountManager.GetForPartner();
            if (response.Succeeded)
            {
                Discounts = response.Data;
            }
        }

        private async Task SaveAsync()
        {
            var prodPriceRequest = new ProductPriceRequest()
            {
                Id = ProductPriceResponse.Id,
                Price = ProductPriceResponse.Price,
                OldPrice = ProductPriceResponse.OldPrice,
                Cost = ProductPriceResponse.Cost,
                DiscountId = _discountId,
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
