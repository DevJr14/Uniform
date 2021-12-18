using Application.Identity.Commands.User;
using Application.Identity.Queries.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Identity;
using System.Threading.Tasks;

namespace Api.Controllers.Identity
{
    public class UsersController : BaseApiController<UsersController>
    {

        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet("identity/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet("identity/[controller]/get-by/{userid}")]
        public async Task<IActionResult> GetById(string userid)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery() { UserId = userid }));
        }

        [Authorize(Policy = Permissions.Roles.View)]
        [HttpGet("identity/[controller]/get-roles/{id}")]
        public async Task<IActionResult> GetUserRolesAsync(string id)
        {
            return Ok(await _mediator.Send(new GetUserRolesQuery() { UserId = id }));
        }

        [Authorize(Policy = Permissions.Users.Edit)]
        [HttpPut("identity/[controller]/update-roles/{id}")]
        public async Task<IActionResult> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            return Ok(await _mediator.Send(new UpdateUserRolesCommand() { UpdateUserRolesRequest = request }));
        }

        [AllowAnonymous]
        [HttpPost("identity/[controller]/register")]
        public async Task<IActionResult> PostAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _mediator.Send(new RegisterUserCommand() { RegisterRequest = request, Origin = origin }));
        }

        [HttpGet("identity/[controller]/confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            return Ok(await _mediator.Send(new EmailConfirmationCommand() {UserId = userId, Code = code }));
        }

        [Authorize(Policy = Permissions.Users.Edit)]
        [HttpPost("identity/[controller]/toggle-status")]
        public async Task<IActionResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        {
            return Ok(await _mediator.Send(new UpdateUserStatusCommand() { UserStatusRequest = request }));
        }

        [HttpPost("identity/[controller]/forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _mediator.Send(new ForgotPasswordCommand() { ForgotPasswordRequest = request, Origin = origin }));
        }

        [HttpPost("identity/[controller]/reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _mediator.Send(new ResetPasswordCommand() { ResetPasswordRequest = request }));
        }

        [Authorize(Policy = Permissions.Users.Export)]
        [HttpGet("identity/[controller]/export")]
        public async Task<IActionResult> Export()
        {
            return Ok(await _mediator.Send(new ExportUsersQuery()));
        }
    }
}
