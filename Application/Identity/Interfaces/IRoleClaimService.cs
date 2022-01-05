using SharedR.Requests.Identity;
using SharedR.Responses.Identity;
using SharedR.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Identity.Interfaces
{
    public interface IRoleClaimService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();
        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);
        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
        Task<int> GetCountAsync();
    }
}
