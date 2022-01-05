using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Products.Commands
{
    public class DeleteProductCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteProductCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var productInDb = await _unitOfWork.RepositoryFor<Product>().GetByIdAsync(command.Id);
            if (productInDb != null && productInDb.DeletedBy == null)
            {
                productInDb.DeletedBy = _currentUser.UserId;
                productInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Product>().MarkDeletedAsync(productInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(productInDb.Id, "Product Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Product with Id: {command.Id} Not Found.");
        }
    }
}
