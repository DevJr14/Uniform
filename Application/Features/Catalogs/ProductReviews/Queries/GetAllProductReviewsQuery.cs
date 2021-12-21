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

namespace Application.Features.Catalogs.ProductReviews.Queries
{
    public class GetAllProductReviewsQuery : IRequest<Result<List<ProductReviewResponse>>>
    {
        public Guid ProductId { get; set; }
    }

    internal class GetAllProductReviewsQueryHandler : IRequestHandler<GetAllProductReviewsQuery, Result<List<ProductReviewResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllProductReviewsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductReviewResponse>>> Handle(GetAllProductReviewsQuery query, CancellationToken cancellationToken)
        {
            var productReviewReviews = _unitOfWork.RepositoryFor<ProductReview>().Entities
                .Where(p => p.ProductId == query.ProductId && p.DeletedBy == null)
                .ToList();
            if (productReviewReviews.Count > 0)
            {
                var mappedProductReviews = _mapper.Map<List<ProductReviewResponse>>(productReviewReviews);
                return await Result<List<ProductReviewResponse>>.SuccessAsync(mappedProductReviews);
            }
            return await Result<List<ProductReviewResponse>>.FailAsync($"Product With Id: {query.ProductId} Has No Reviews.");
        }
    }
}
