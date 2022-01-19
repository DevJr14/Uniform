using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Promotions;
using MediatR;
using SharedR.Responses.Promotions;
using SharedR.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Promotions.Discounts.Queries
{
    public class GetDiscountByIdQuery : IRequest<Result<DiscountResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, Result<DiscountResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetDiscountByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<DiscountResponse>> Handle(GetDiscountByIdQuery query, CancellationToken cancellationToken)
        {
            var discountInDb = await _unitOfWork.RepositoryFor<Discount>().GetByIdAsync(query.Id);
            if (discountInDb != null && discountInDb.DeletedBy == null)
            {
                var mappedDiscount = _mapper.Map<DiscountResponse>(discountInDb);
                return await Result<DiscountResponse>.SuccessAsync(mappedDiscount);
            }
            return await Result<DiscountResponse>.FailAsync($"Discount with Id: {query.Id} No Found.");
        }
    }
}
