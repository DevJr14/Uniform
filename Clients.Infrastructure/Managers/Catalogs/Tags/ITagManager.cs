using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.Tags
{
    public interface ITagManager
    {
        Task<IResult<List<TagResponse>>> GetAll();
        Task<IResult<List<TagResponse>>> GetForPartner();
        Task<IResult<TagResponse>> GetById(Guid tagId);
        Task<IResult<List<TagResponse>>> GetForUser(Guid userId);
        Task<IResult<Guid>> Save(TagRequest tagRequest);
        Task<IResult<Guid>> Delete(Guid tagId);
    }
}
