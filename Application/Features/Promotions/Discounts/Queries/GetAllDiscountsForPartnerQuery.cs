using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
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
    public class GetAllDiscountsForPartnerQuery : IRequest<Result<List<DiscountResponse>>>
    {
        //To Do: A User can have multiple Partnerships. Partner Id will need to be provided in this query.
    }

    internal class GetAllDiscountsForPartnerQueryHandler : IRequestHandler<GetAllDiscountsForPartnerQuery, Result<List<DiscountResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetAllDiscountsForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<List<DiscountResponse>>> Handle(GetAllDiscountsForPartnerQuery query, CancellationToken cancellationToken)
        {
            var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                          .Where(p => p.UserId == new Guid(_currentUser.UserId))
                          .FirstOrDefault();
            if (partner != null)
            {
                var discounts = _unitOfWork.RepositoryFor<Discount>().Entities
                    .Where(d => d.PartnerId == partner.Id && d.DeletedBy == null)
                    .ToList();
                if (discounts.Count > 0)
                {
                    var mappedDiscounts = _mapper.Map<List<DiscountResponse>>(discounts.Where(a => a.DeletedBy == null));
                    return await Result<List<DiscountResponse>>.SuccessAsync(mappedDiscounts);
                }
                return await Result<List<DiscountResponse>>.FailAsync("No Records Found.");
            }
            return await Result<List<DiscountResponse>>.FailAsync("Partner Profile Not Found.");
        }
    }
}
