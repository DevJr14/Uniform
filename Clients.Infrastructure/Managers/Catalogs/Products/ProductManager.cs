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

namespace Clients.Infrastructure.Managers.Catalogs.Products
{
    public class ProductManager : IProductManager
    {
        private readonly HttpClient _httpClient;

        public ProductManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"{ProductsEndpoints.Delete}{productId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<ProductResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(ProductsEndpoints.GetAll);
            return await response.ToResult<List<ProductResponse>>();
        }

        public async Task<IResult<ProductResponse>> GetById(Guid productId)
        {
            var response = await _httpClient.GetAsync(ProductsEndpoints.GetById(productId));
            return await response.ToResult<ProductResponse>();
        }

        public async Task<IResult<List<ProductResponse>>> GetForPartner()
        {
            var response = await _httpClient.GetAsync(ProductsEndpoints.GetForPartner());
            return await response.ToResult<List<ProductResponse>>();
        }

        public async Task<IResult<List<ProductResponse>>> GetForUser(Guid userId)
        {
            var response = await _httpClient.GetAsync(ProductsEndpoints.GetForUser(userId));
            return await response.ToResult<List<ProductResponse>>();
        }

        public async Task<IResult<Guid>> Save(ProductRequest productRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(ProductsEndpoints.Save, productRequest);
            return await response.ToResult<Guid>();
        }
    }
}
