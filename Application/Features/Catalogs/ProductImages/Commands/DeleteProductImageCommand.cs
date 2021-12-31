using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductImages.Commands
{
    public class DeleteProductImageCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteProductImageCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteProductImageCommand command, CancellationToken cancellationToken)
        {
            var productImageInDb = await _unitOfWork.RepositoryFor<ProductImage>().GetByIdAsync(command.Id);
            if (productImageInDb != null && productImageInDb.DeletedBy == null)
            {
                productImageInDb.DeletedBy = _currentUser.UserId;
                productImageInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<ProductImage>().MarkDeletedAsync(productImageInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(productImageInDb.Id, "Product Image Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Product Image with Id: {command.Id} Not Found.");
        }
    }
}
