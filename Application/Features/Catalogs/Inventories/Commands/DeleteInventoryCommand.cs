using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Inventories.Commands
{
    public class DeleteInventoryCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteInventoryCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteInventoryCommand command, CancellationToken cancellationToken)
        {
            var inventoryInDb = await _unitOfWork.RepositoryFor<Inventory>().Entities
                .Where(i => i.DeletedBy == null)
                .FirstOrDefaultAsync();
            if (inventoryInDb != null)
            {
                inventoryInDb.DeletedBy = _currentUser.UserId;
                inventoryInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Inventory>().MarkDeletedAsync(inventoryInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(inventoryInDb.Id, "Inventory Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Inventory with Id: {command.Id} Not Found.");
        }
    }
}
