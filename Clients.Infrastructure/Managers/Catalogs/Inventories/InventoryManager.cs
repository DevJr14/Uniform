using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Catalog;
using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.Inventories
{
    public class InventoryManager : IInventoryManager
    {
        private readonly HttpClient _httpClient;

        public InventoryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid inventoryId)
        {
            var response = await _httpClient.DeleteAsync($"{InventoriesEndpoints.Delete}{inventoryId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<InventoryResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(InventoriesEndpoints.GetAll);
            return await response.ToResult<List<InventoryResponse>>();
        }

        public async Task<IResult<InventoryResponse>> GetForProduct(Guid productId)
        {
            var response = await _httpClient.GetAsync(InventoriesEndpoints.GetForProduct(productId));
            return await response.ToResult<InventoryResponse>();
        }

        public async Task<IResult<Guid>> Save(InventoryRequest inventoryRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(InventoriesEndpoints.Save, inventoryRequest);
            return await response.ToResult<Guid>();
        }
    }
}
