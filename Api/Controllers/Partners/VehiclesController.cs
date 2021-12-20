using Application.Features.Partners.Vehicles.Commands;
using Application.Features.Partners.Vehicles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Partners;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Partners
{
    public class VehiclesController : BaseApiController<VehiclesController>
    {
        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllVehiclesQuery()));
        }

        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-all-for-partner")]
        public async Task<IActionResult> GetAllForPartner(Guid partnerId)
        {
            return Ok(await _mediator.Send(new GetAllVehiclesForPartnerQuery() { PartnerId = partnerId }));
        }

        [Authorize(Policy = Permissions.Partners.View)]
        [HttpGet("partnership/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetVehicleByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.Partners.Create)]
        [HttpPost("partnership/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(VehicleRequest request)
{
            return Ok(await _mediator.Send(new AddEditVehicleCommand() { VehicleRequest = request }));
        }

        [Authorize(Policy = Permissions.Partners.Delete)]
        [HttpDelete("partnership/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteVehicleCommand() { Id = id }));
        }
    }
}
