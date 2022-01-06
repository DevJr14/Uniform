using SharedR.Wrapper;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Partnerships.Address
{
    public interface IAddressManager : IManager
    {
        Task<IResult<List<AddressResponse>>> GetAll();
        Task<IResult<List<AddressResponse>>> GetForPartner(Guid partnerId);
        Task<IResult<AddressResponse>> GetById(Guid addressId);
        Task<IResult<List<AddressResponse>>> GetForUser(Guid userId);
        Task<IResult<Guid>> Save(AddressRequest addressRequest);
        Task<IResult<Guid>> Delete(Guid addressId);
    }
}
