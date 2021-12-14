using Application.Identity.Commands.Role;
using Application.Identity.Queries.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using Shared.Requests.Identity;
using System.Threading.Tasks;

namespace Api.Controllers.Identity
{
    public class RoleController : BaseApiController<RoleController>
    {
        /// <summary>
        /// Get All Roles (basic, admin etc.)
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.View)]
        [HttpGet("identity/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        /// <summary>
        /// Add a Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.Create)]
        [HttpPost("identity/[controller]/save")]
        public async Task<IActionResult> Post(RoleRequest request)
        {
            var response = await _mediator.Send(new AddEditRoleCommand() { RoleRequest = request });
            return Ok(response);
        }

        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.Delete)]
        [HttpDelete("identity/[controller]/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteRoleCommand() { RoleId = id });
            return Ok(response);
        }

        /// <summary>
        /// Get Permissions By Role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.RoleClaims.View)]
        [HttpGet("identity/[controller]/permissions/{roleId}")]
        public async Task<IActionResult> GetPermissionsByRoleId([FromRoute] string roleId)
        {
            var response = await _mediator.Send(new GetAllPermissionsByRoleIdQuery() { RoleId = roleId });
            return Ok(response);
        }

        /// <summary>
        /// Edit a Role Claim
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.RoleClaims.Edit)]
        [HttpPut("identity/[controller]/permissions/update")]
        public async Task<IActionResult> Update(PermissionRequest request)
        {
            var response = await _mediator.Send(new UpdateRolePermissionsCommand() { PermissionRequest = request });
            return Ok(response);
        }
    }
}
