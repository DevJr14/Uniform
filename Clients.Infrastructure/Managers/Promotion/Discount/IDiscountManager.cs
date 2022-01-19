using SharedR.Requests.Promotions;
using SharedR.Responses.Promotions;
using SharedR.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Promotion.Discount
{
    public interface IDiscountManager : IManager
    {
        Task<IResult<List<DiscountResponse>>> GetAll();
        Task<IResult<List<DiscountResponse>>> GetForPartner();
        Task<IResult<DiscountResponse>> GetById(Guid discountId);
        Task<IResult<Guid>> Save(DiscountRequest discountRequest);
        Task<IResult<Guid>> Delete(Guid discountId);
    }
}
