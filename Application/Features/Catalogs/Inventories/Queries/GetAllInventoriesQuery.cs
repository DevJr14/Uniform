using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Inventories.Queries
{
    public class GetAllInventoriesQuery : IRequest<Result<List<InventoryResponse>>>
    {
    }

    internal class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoriesQuery, Result<List<InventoryResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllInventoryQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<InventoryResponse>>> Handle(GetAllInventoriesQuery query, CancellationToken cancellationToken)
        {
            var inventories = _unitOfWork.RepositoryFor<Inventory>().Entities
                .Where(p => p.DeletedBy == null)
                .ToList();
            if (inventories.Count > 0)
            {
                var mappedInventories = _mapper.Map<List<InventoryResponse>>(inventories);
                return await Result<List<InventoryResponse>>.SuccessAsync(mappedInventories);
            }
            return await Result<List<InventoryResponse>>.FailAsync($"No Inventories Found.");
        }
    }
}
