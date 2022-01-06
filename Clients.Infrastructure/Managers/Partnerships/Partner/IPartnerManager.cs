using SharedR.Requests.Partners;
using SharedR.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedR.Responses.Partners;

namespace Clients.Infrastructure.Managers.Partnerships.Partner
{
    public interface IPartnerManager : IManager
    {
        Task<IResult<List<PartnerResponse>>> GetAll();
        Task<IResult<List<PartnerResponse>>> GetByVerification(bool isVerified);
        Task<IResult<PartnerResponse>> GetById(Guid partnerId);
        Task<IResult<List<PartnerResponse>>> GetForUser(Guid userId);
        Task<IResult<Guid>> Save(PartnerRequest partnerRequest);
        Task<IResult<Guid>> Delete(Guid partnerId);
        Task<IResult<Guid>> ActivateDeActiate(Guid partnerId);
    }
}
