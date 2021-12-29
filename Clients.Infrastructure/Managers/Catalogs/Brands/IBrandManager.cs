using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.Brands
{
    public interface IBrandManager : IManager
    {
        Task<IResult<List<BrandResponse>>> GetAll();
        Task<IResult<List<BrandResponse>>> GetForPartner();
        Task<IResult<BrandResponse>> GetById(Guid brandId);
        Task<IResult<List<BrandResponse>>> GetForUser(Guid userId);
        Task<IResult<Guid>> Save(BrandRequest brandRequest);
        Task<IResult<Guid>> Delete(Guid brandId);
    }
}
