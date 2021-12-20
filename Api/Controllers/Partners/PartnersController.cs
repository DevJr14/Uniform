using Application.Features.Partners.Commands;
using Application.Features.Partners.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Partners;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Partners
{
    public class PartnersController : BaseApiController<PartnersController>
    {
        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllPartnersQuery()));
        }

        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-for-user")]
        public async Task<IActionResult> GetForUser(Guid userId)
        {
            return Ok(await _mediator.Send(new GetPartnerForUserQuery() { UserId = userId }));
        }

        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetPartnerByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-by-verification-status")]
        public async Task<IActionResult> GetByVerificationStatus(bool isVerified)
        {
            return Ok(await _mediator.Send(new GetPartnersByVerificationStatusQuery() { IsVerified = isVerified }));
        }

        [Authorize(Policy = Permissions.Partners.Create)]
        [HttpPost("partnership/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(PartnerRequest request)
        {
            return Ok(await _mediator.Send(new AddEditPartnerCommand() { PartnerRequest = request }));
        }

        [Authorize(Policy = Permissions.Partners.Delete)]
        [HttpDelete("partnership/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeletePartnerCommand() { Id = id }));
        }

        [Authorize(Policy = Permissions.Partners.Activate)]
        [HttpGet("partnership/[controller]/activate-de-activate")]
        public async Task<IActionResult> ActivateDeActivate(Guid id)
        {
            return Ok(await _mediator.Send(new ActivateDeActivatePartnerCommand() { Id = id }));
        }

    }
}
