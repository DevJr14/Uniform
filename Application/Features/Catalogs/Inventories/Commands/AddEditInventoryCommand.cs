using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Inventories.Commands
{
    public class AddEditInventoryCommand : IRequest<Result<Guid>>
    {
        public InventoryRequest InventoryRequest { get; set; }
    }

    internal class AddEditInventoryCommandHandler : IRequestHandler<AddEditInventoryCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        public AddEditInventoryCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddEditInventoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.InventoryRequest.Id == Guid.Empty)
                {
                    var inventory = _mapper.Map<Inventory>(command.InventoryRequest);
                    await _unitOfWork.RepositoryFor<Inventory>().AddAsync(inventory);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(inventory.Id, "Inventory Saved Successfully.");
                }
                else
                {
                    var inventoryInDb = await _unitOfWork.RepositoryFor<Inventory>().GetByIdAsync(command.InventoryRequest.Id);
                    if (inventoryInDb != null && inventoryInDb.DeletedBy == null)
                    {
                        inventoryInDb.StockQty = command.InventoryRequest.StockQty;
                        inventoryInDb.AvailableQty = command.InventoryRequest.AvailableQty;
                        inventoryInDb.MinCartQty = command.InventoryRequest.MinCartQty;
                        inventoryInDb.MaxCartQty = command.InventoryRequest.MaxCartQty;
                        inventoryInDb.NotifyQtyBelow = command.InventoryRequest.NotifyQtyBelow;
                        inventoryInDb.DisplayQty = command.InventoryRequest.DisplayQty;
                        inventoryInDb.IsReturnable = command.InventoryRequest.IsReturnable;

                        await _unitOfWork.RepositoryFor<Inventory>().UpdateAsync(inventoryInDb);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<Guid>.SuccessAsync(inventoryInDb.Id, "Inventory Updated Successfully.");
                    }
                    return await Result<Guid>.FailAsync("Inventory Not Found.");
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
