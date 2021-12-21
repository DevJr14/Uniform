using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductReviews.Queries
{
    public class GetProductReviewByIdQuery : IRequest<Result<ProductReviewResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetProductReviewByIdQueryHandler : IRequestHandler<GetProductReviewByIdQuery, Result<ProductReviewResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetProductReviewByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductReviewResponse>> Handle(GetProductReviewByIdQuery query, CancellationToken cancellationToken)
        {
            var productReviewInDb = await _unitOfWork.RepositoryFor<ProductReview>().GetByIdAsync(query.Id);
            if (productReviewInDb != null && productReviewInDb.DeletedBy == null)
            {
                var mappedProductReview = _mapper.Map<ProductReviewResponse>(productReviewInDb);
                return await Result<ProductReviewResponse>.SuccessAsync(mappedProductReview);
            }
            return await Result<ProductReviewResponse>.FailAsync($"Product Review with Id: {query.Id} No Found.");
        }
    }
}
