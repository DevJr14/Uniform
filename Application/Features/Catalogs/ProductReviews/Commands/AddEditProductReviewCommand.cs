using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductReviews.Commands
{
    public class AddEditProductReviewCommand : IRequest<Result<Guid>>
    {
        public ProductReviewRequest ProductReviewRequest { get; set; }
    }

    internal class AddEditProductReviewCommandHandler : IRequestHandler<AddEditProductReviewCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        public AddEditProductReviewCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddEditProductReviewCommand command, CancellationToken cancellationToken)
        {
            if (command.ProductReviewRequest.Id == Guid.Empty)
            {
                var productReview = _mapper.Map<ProductReview>(command.ProductReviewRequest);
                await _unitOfWork.RepositoryFor<ProductReview>().AddAsync(productReview);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(productReview.Id, "Product Review Saved Successfully.");
            }
            else
            {
                var productReviewInDb = await _unitOfWork.RepositoryFor<ProductReview>().GetByIdAsync(command.ProductReviewRequest.Id);
                if (productReviewInDb != null && productReviewInDb.DeletedBy == null)
                {
                    productReviewInDb.ReviewText = command.ProductReviewRequest.ReviewText;
                    productReviewInDb.Rating = command.ProductReviewRequest.Rating;
                    productReviewInDb.ReplyText = command.ProductReviewRequest.ReplyText;

                    await _unitOfWork.RepositoryFor<ProductReview>().UpdateAsync(productReviewInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(productReviewInDb.Id, "Product Review Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Product Review Not Found.");
            }
        }
    }
}
