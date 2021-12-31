using Clients.Infrastructure.Managers.Catalogs.Inventories;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Inventories
{
    public partial class AddEditInventory
    {
        [Inject] private IInventoryManager InventoryManager { get; set; }
        [Parameter] public Guid ProductId { get; set; }
        public InventoryResponse InventoryResponse { get; set; } = new();

        private bool _isLoaded;

        protected override async Task OnInitializedAsync()
        {
            var response = await InventoryManager.GetForProduct(ProductId);
            if (response.Succeeded)
            {
                InventoryResponse = response.Data;
            }
            _isLoaded = true;
        }

        private async Task SaveAsync()
        {
            var inventoryRequest = new InventoryRequest()
            {
                Id = InventoryResponse.Id,                
                ProductId = ProductId,
                StockQty = InventoryResponse.StockQty,
                AvailableQty = InventoryResponse.AvailableQty,
                DisplayQty = InventoryResponse.DisplayQty,
                MinCartQty = InventoryResponse.MinCartQty,
                MaxCartQty = InventoryResponse.MaxCartQty,
                NotifyQtyBelow = InventoryResponse.NotifyQtyBelow,
                IsReturnable = InventoryResponse.IsReturnable
            };
            var response = await InventoryManager.Save(inventoryRequest);
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
