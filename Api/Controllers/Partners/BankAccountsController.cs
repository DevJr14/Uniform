using Application.Features.Partners.BankAccounts.Commands;
using Application.Features.Partners.BankAccounts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Partners;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Partners
{
    public class BankAccountsController : BaseApiController<BankAccountsController>
    {
        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllBankAccountsQuery()));
        }

        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-all-for-partner")]
        public async Task<IActionResult> GetAllForPartner(Guid partnerId)
        {
            return Ok(await _mediator.Send(new GetAllBankAccountsForPartnerQuery() { PartnerId = partnerId }));
        }

        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetBankAccountByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.Partners.Create)]
        [HttpPost("partnership/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(BankAccountRequest request)
{
            return Ok(await _mediator.Send(new AddEditBankAccountCommand() { BankAccountRequest = request }));
        }

        [Authorize(Policy = Permissions.Partners.Delete)]
        [HttpDelete("partnership/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteBankAccountCommand() { Id = id }));
        }
    }
}
