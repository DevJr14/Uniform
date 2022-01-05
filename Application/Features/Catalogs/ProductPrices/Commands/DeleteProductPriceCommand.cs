using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductPrices.Commands
{
    public class DeleteProductPriceCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteProductPriceCommandHandler : IRequestHandler<DeleteProductPriceCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteProductPriceCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteProductPriceCommand command, CancellationToken cancellationToken)
        {
            var productPriceInDb = await _unitOfWork.RepositoryFor<ProductPrice>().GetByIdAsync(command.Id);
            if (productPriceInDb != null && productPriceInDb.DeletedBy == null)
            {
                productPriceInDb.DeletedBy = _currentUser.UserId;
                productPriceInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<ProductPrice>().MarkDeletedAsync(productPriceInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(productPriceInDb.Id, "Product Price Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Product Price with Id: {command.Id} Not Found.");
        }
    }
}
