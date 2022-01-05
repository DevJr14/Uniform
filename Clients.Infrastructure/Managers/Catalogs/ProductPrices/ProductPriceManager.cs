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

namespace Clients.Infrastructure.Managers.Catalogs.ProductPrices
{
    public class ProductPriceManager : IProductPriceManager
    {
        private readonly HttpClient _httpClient;

        public ProductPriceManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<Guid>> Delete(Guid productPriceId)
        {
            var response = await _httpClient.DeleteAsync($"{ProductPriceEndpoints.Delete}{productPriceId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<ProductPriceResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(ProductPriceEndpoints.GetAll);
            return await response.ToResult<List<ProductPriceResponse>>();
        }

        public async Task<IResult<ProductPriceResponse>> GetById(Guid productPriceId)
        {
            var response = await _httpClient.GetAsync(ProductPriceEndpoints.GetById(productPriceId));
            return await response.ToResult<ProductPriceResponse>();
        }

        public async Task<IResult<ProductPriceResponse>> GetForProduct(Guid productId)
        {
            var response = await _httpClient.GetAsync(ProductPriceEndpoints.GetForProduct(productId));
            return await response.ToResult<ProductPriceResponse>();
        }

        public async Task<IResult<Guid>> Save(ProductPriceRequest productPriceRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(ProductPriceEndpoints.Save, productPriceRequest);
            return await response.ToResult<Guid>();
        }
    }
}
