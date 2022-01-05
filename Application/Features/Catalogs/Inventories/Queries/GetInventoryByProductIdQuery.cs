using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedR.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Inventories.Queries
{
    public class GetInventoryByProductIdQuery : IRequest<Result<InventoryResponse>>
    {
        public Guid ProductId { get; set; }
    }

    internal class GetInventoryByProductIdQueryHandler : IRequestHandler<GetInventoryByProductIdQuery, Result<InventoryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetInventoryByProductIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<InventoryResponse>> Handle(GetInventoryByProductIdQuery query, CancellationToken cancellationToken)
        {
            var inventoryInDb = await _unitOfWork.RepositoryFor<Inventory>().Entities
                .Where(i => i.ProductId == query.ProductId && i.DeletedBy == null)
                .FirstOrDefaultAsync();

            if (inventoryInDb != null)
            {
                var mappedInventory = _mapper.Map<InventoryResponse>(inventoryInDb);
                return await Result<InventoryResponse>.SuccessAsync(mappedInventory);
            }
            return await Result<InventoryResponse>.FailAsync($"Product with Id: {query.ProductId} has no inventory.");
        }
    }
}
