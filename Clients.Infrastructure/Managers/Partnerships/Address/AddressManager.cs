using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Partnership;
using SharedR.Wrapper;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Partnerships.Address
{
    public class AddressManager : IAddressManager
    {
        private readonly HttpClient _httpClient;

        public AddressManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<Guid>> Delete(Guid addressId)
        {
            var response = await _httpClient.DeleteAsync($"{AddressEndpoints.Delete}{addressId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<AddressResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(AddressEndpoints.GetAll);
            return await response.ToResult<List<AddressResponse>>();
        }

        public async Task<IResult<AddressResponse>> GetById(Guid addressId)
        {
            var response = await _httpClient.GetAsync(AddressEndpoints.GetById(addressId));
            return await response.ToResult<AddressResponse>();
        }

        public async Task<IResult<List<AddressResponse>>> GetForPartner(Guid partnerId)
        {
            var response = await _httpClient.GetAsync(AddressEndpoints.GetForPartner(partnerId));
            return await response.ToResult<List<AddressResponse>>();
        }

        public async Task<IResult<List<AddressResponse>>> GetForUser(Guid userId)
        {
            var response = await _httpClient.GetAsync(AddressEndpoints.GetForUser(userId));
            return await response.ToResult<List<AddressResponse>>();
        }

        public async Task<IResult<Guid>> Save(AddressRequest addressRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(AddressEndpoints.Save, addressRequest);
            return await response.ToResult<Guid>();
        }
    }
}
