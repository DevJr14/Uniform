using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.ProductPrices
{
    public interface IProductPriceManager : IManager
    {
        Task<IResult<List<ProductPriceResponse>>> GetAll();
        Task<IResult<ProductPriceResponse>> GetForProduct(Guid productId);
        Task<IResult<ProductPriceResponse>> GetById(Guid productPriceId);
        Task<IResult<Guid>> Save(ProductPriceRequest productPriceRequest);
        Task<IResult<Guid>> Delete(Guid productPriceId);
    }
}
