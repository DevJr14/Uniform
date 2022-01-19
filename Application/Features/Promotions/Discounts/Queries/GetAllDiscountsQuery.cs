using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Promotions;
using MediatR;
using SharedR.Responses.Promotions;
using SharedR.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Promotions.Discounts.Queries
{
    public class GetAllDiscountsQuery : IRequest<Result<List<DiscountResponse>>>
    {
    }

    internal class GetAllDiscountsQueryHandler : IRequestHandler<GetAllDiscountsQuery, Result<List<DiscountResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllDiscountsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<DiscountResponse>>> Handle(GetAllDiscountsQuery query, CancellationToken cancellationToken)
        {
            var discounts = _unitOfWork.RepositoryFor<Discount>().Entities
                .Where(p => p.DeletedBy == null)
                .ToList();
            if (discounts.Count > 0)
            {
                var mappedDiscounts = _mapper.Map<List<DiscountResponse>>(discounts);
                return await Result<List<DiscountResponse>>.SuccessAsync(mappedDiscounts);
            }
            return await Result<List<DiscountResponse>>.FailAsync("No Discounts Exists.");
        }
    }
}
