using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Partnership;
using SharedR.Wrapper;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Partnerships.BankAccount
{
    public class BankAccountManager : IBankAccountManager
    {
        private readonly HttpClient _httpClient;

        public BankAccountManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid bankAccountId)
        {
            var response = await _httpClient.DeleteAsync($"{BankAccountEndpoints.Delete}{bankAccountId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<BankAccountResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(BankAccountEndpoints.GetAll);
            return await response.ToResult<List<BankAccountResponse>>();
        }

        public async Task<IResult<BankAccountResponse>> GetById(Guid bankAccountId)
        {
            var response = await _httpClient.GetAsync(BankAccountEndpoints.GetById(bankAccountId));
            return await response.ToResult<BankAccountResponse>();
        }

        public async Task<IResult<List<BankAccountResponse>>> GetForPartner(Guid partnerId)
        {
            var response = await _httpClient.GetAsync(BankAccountEndpoints.GetForPartner(partnerId));
            return await response.ToResult<List<BankAccountResponse>>();
        }

        public async Task<IResult<List<BankAccountResponse>>> GetForUser(Guid userId)
        {
            var response = await _httpClient.GetAsync(BankAccountEndpoints.GetForUser(userId));
            return await response.ToResult<List<BankAccountResponse>>();
        }

        public async Task<IResult<Guid>> Save(BankAccountRequest bankAccountRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(BankAccountEndpoints.Save, bankAccountRequest);
            return await response.ToResult<Guid>();
        }
    }
}
