using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Catalog;
using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.Brands
{
    public class BrandManager : IBrandManager
    {
        private readonly HttpClient _httpClient;

        public BrandManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid brandId)
        {
            var response = await _httpClient.DeleteAsync($"{BrandsEndpoints.Delete}{brandId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<BrandResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(BrandsEndpoints.GetAll);
            return await response.ToResult<List<BrandResponse>>();
        }

        public async Task<IResult<BrandResponse>> GetById(Guid brandId)
        {
            var response = await _httpClient.GetAsync(BrandsEndpoints.GetById(brandId));
            return await response.ToResult<BrandResponse>();
        }

        public async Task<IResult<List<BrandResponse>>> GetForPartner()
        {
            var response = await _httpClient.GetAsync(BrandsEndpoints.GetForPartner());
            return await response.ToResult<List<BrandResponse>>();
        }

        public async Task<IResult<List<BrandResponse>>> GetForUser(Guid userId)
        {
            var response = await _httpClient.GetAsync(BrandsEndpoints.GetForUser(userId));
            return await response.ToResult<List<BrandResponse>>();
        }

        public async Task<IResult<Guid>> Save(BrandRequest brandRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(BrandsEndpoints.Save, brandRequest);
            return await response.ToResult<Guid>();
        }
    }
}
