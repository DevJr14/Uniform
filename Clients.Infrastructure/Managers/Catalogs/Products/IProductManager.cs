using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.Products
{
    public interface IProductManager
    {
        Task<IResult<List<ProductResponse>>> GetAll();
        Task<IResult<List<ProductResponse>>> GetForPartner();
        Task<IResult<ProductResponse>> GetById(Guid productId);
        Task<IResult<List<ProductResponse>>> GetForUser(Guid userId);
        Task<IResult<Guid>> Save(ProductRequest productRequest);
        Task<IResult<Guid>> Delete(Guid productId);
    }
}
