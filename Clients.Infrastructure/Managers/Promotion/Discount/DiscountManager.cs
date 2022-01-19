using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Promotion;
using SharedR.Requests.Promotions;
using SharedR.Responses.Promotions;
using SharedR.Wrapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Promotion.Discount
{
    public class DiscountManager : IDiscountManager
    {
        private readonly HttpClient _httpClient;

        public DiscountManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid discountId)
        {
            var response = await _httpClient.DeleteAsync($"{DiscountEndpoints.Delete}{discountId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<DiscountResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(DiscountEndpoints.GetAll);
            return await response.ToResult<List<DiscountResponse>>();
        }

        public async Task<IResult<DiscountResponse>> GetById(Guid discountId)
        {
            var response = await _httpClient.GetAsync(DiscountEndpoints.GetById(discountId));
            return await response.ToResult<DiscountResponse>();
        }

        public async Task<IResult<List<DiscountResponse>>> GetForPartner()
        {
            var response = await _httpClient.GetAsync(DiscountEndpoints.GetForPartner());
            return await response.ToResult<List<DiscountResponse>>();
        }

        public async Task<IResult<Guid>> Save(DiscountRequest discountRequest)
{
            var response = await _httpClient.PostAsJsonAsync(DiscountEndpoints.Save, discountRequest);
            return await response.ToResult<Guid>();
        }
    }
}
