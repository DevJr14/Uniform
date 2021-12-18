using Application.Identity.Commands.RoleClaim;
using Application.Identity.Queries.RoleClaim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Identity;
using System.Threading.Tasks;

namespace Api.Controllers.Identity
{
    public class RoleClaimController : BaseApiController<RoleClaimController>
    {

        /// <summary>
        /// Get All Role Claims(e.g. Product Create Permission)
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.RoleClaims.View)]
        [HttpGet("identity/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            var roleClaims = await _mediator.Send(new GetAllRoleClaimsQuery());
            return Ok(roleClaims);
        }

        /// <summary>
        /// Get All Role Claims By Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.RoleClaims.View)]
        [HttpGet("identity/[controller]/get-by/{roleId}")]
        public async Task<IActionResult> GetAllByRoleId([FromRoute] string roleId)
        {
            var response = await _mediator.Send(new GetAllRoleClaimsByRoleId() { RoleId = roleId });
            return Ok(response);
        }

        /// <summary>
        /// Add a Role Claim
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK </returns>
        [Authorize(Policy = Permissions.RoleClaims.Create)]
        [HttpPost("identity/[controller]/save")]
        public async Task<IActionResult> Post(RoleClaimRequest request)
        {
            var response = await _mediator.Send(new AddUpdateRoleClaimsCommand() { RoleClaimRequest = request });
            return Ok(response);
        }

        /// <summary>
        /// Delete a Role Claim
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.RoleClaims.Delete)]
        [HttpDelete("identity/[controller]/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteRoleClaimCommand() { Id = id });
            return Ok(response);
        }
    }
}
