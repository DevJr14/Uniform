using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.Inventories
{
    public interface IInventoryManager : IManager
    {
        Task<IResult<List<InventoryResponse>>> GetAll();
        Task<IResult<InventoryResponse>> GetForProduct(Guid productId);
        Task<IResult<Guid>> Save(InventoryRequest inventoryRequest);
        Task<IResult<Guid>> Delete(Guid inventoryId);
    }
}
