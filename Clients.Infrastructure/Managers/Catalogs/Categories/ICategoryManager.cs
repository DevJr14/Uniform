using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.Categories
{
    public interface ICategoryManager : IManager
    {
        Task<IResult<List<CategoryResponse>>> GetAll();
        Task<IResult<List<CategoryResponse>>> GetForPartner();
        Task<IResult<CategoryResponse>> GetById(Guid categoryId);
        Task<IResult<List<CategoryResponse>>> GetForUser(Guid userId);
        Task<IResult<Guid>> Save(CategoryRequest categoryRequest);
        Task<IResult<Guid>> Delete(Guid categoryId);
    }
}
