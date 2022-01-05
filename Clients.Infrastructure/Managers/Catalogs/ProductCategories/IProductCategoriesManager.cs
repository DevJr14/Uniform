using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.ProductCategories
{
    public interface IProductCategoriesManager : IManager
    {
        Task<IResult<List<ProductCategoriesResponse>>> GetForProduct(Guid productId);
        Task<IResult<Guid>> Save(ProductCategoriesRequest productCategoriesRequest);
    }
}
