using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductReviews.Commands
{
    public class DeleteProductReviewCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteProductReviewCommandHandler : IRequestHandler<DeleteProductReviewCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteProductReviewCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteProductReviewCommand command, CancellationToken cancellationToken)
        {
            var productReviewInDb = await _unitOfWork.RepositoryFor<ProductReview>().GetByIdAsync(command.Id);
            if (productReviewInDb != null && productReviewInDb.DeletedBy == null)
            {
                productReviewInDb.DeletedBy = _currentUser.UserId;
                productReviewInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<ProductReview>().MarkDeletedAsync(productReviewInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(productReviewInDb.Id, "Product Review Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Product Review with Id: {command.Id} Not Found.");
        }
    }
}
