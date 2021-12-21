using Application.Features.Catalogs.Brands.Commands;
using Application.Features.Catalogs.Brands.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class BrandsController : BaseApiController<BrandsController>
    {
        [Authorize(Policy = Permissions.Brands.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllBrandsQuery()));
        }

        [Authorize(Policy = Permissions.Brands.View)]
        [HttpGet("catalog/[controller]/get-all-for-partner")]
        public async Task<IActionResult> GetAllForPartner()
        {
            return Ok(await _mediator.Send(new GetAllBrandsForPartnerQuery()));
        }

        [Authorize(Policy = Permissions.Brands.View)]
        [HttpGet("catalog/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetBrandByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.Brands.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(BrandRequest request)
        {
            return Ok(await _mediator.Send(new AddEditBrandCommand() { BrandRequest = request }));
        }

        [Authorize(Policy = Permissions.Brands.Delete)]
        [HttpDelete("catalog/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteBrandCommand() { Id = id }));
        }
    }
}
