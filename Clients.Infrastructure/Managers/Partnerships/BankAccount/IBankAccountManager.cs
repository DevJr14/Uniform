using Shared.Wrapper;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Partnerships.BankAccount
{
    public interface IBankAccountManager
    {
        Task<IResult<List<BankAccountResponse>>> GetAll();
        Task<IResult<List<BankAccountResponse>>> GetForPartner(Guid partnerId);
        Task<IResult<BankAccountResponse>> GetById(Guid bankAccountId);
        Task<IResult<List<BankAccountResponse>>> GetForUser(Guid userId);
        Task<IResult<Guid>> Save(BankAccountRequest bankAccountRequest);
        Task<IResult<Guid>> Delete(Guid bankAccountId);
    }
}
