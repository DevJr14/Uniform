using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.ProductTags
{
    public interface IProductTagsManager : IManager
    {
        Task<IResult<List<ProductTagsResponse>>> GetForProduct(Guid productId);
        Task<IResult<Guid>> Save(ProductTagsRequest productTagsRequest);
    }
}
