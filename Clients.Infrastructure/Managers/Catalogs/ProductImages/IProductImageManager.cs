using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.ProductImages
{
    public interface IProductImageManager : IManager
    {
        Task<IResult<List<ProductImageResponse>>> GetForProduct(Guid productId);
        Task<IResult<ProductImageResponse>> GetById(Guid productImageId);
        Task<IResult<Guid>> Save(ProductImageRequest productImageRequest);
        Task<IResult<Guid>> Delete(Guid productImageId);
    }
}
