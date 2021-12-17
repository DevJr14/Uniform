using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using Shared.Responses.Partners;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Queries
{
    public class GetPartnersByVerificationStatusQuery : IRequest<Result<List<PartnerResponse>>>
    {
        public bool IsVerified { get; set; }
    }

    internal class GetPartnersByVerificationStatusQueryHandler : IRequestHandler<GetPartnersByVerificationStatusQuery, Result<List<PartnerResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetPartnersByVerificationStatusQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<PartnerResponse>>> Handle(GetPartnersByVerificationStatusQuery query, CancellationToken cancellationToken)
        {
            var partnersInDb = _unitOfWork.RepositoryFor<Partner>()
                .Entities
                .Where(p => p.IsVerified == query.IsVerified && p.DeletedBy == null)
                .ToList();
            if (partnersInDb.Count > 0)
            {
                var mappedPartners = _mapper.Map<List<PartnerResponse>>(partnersInDb);
                return await Result<List<PartnerResponse>>.SuccessAsync(mappedPartners);
            }

            if (query.IsVerified)
            {
                return await Result<List<PartnerResponse>>.FailAsync("No Verified Partners Found.");
            }
            return await Result<List<PartnerResponse>>.FailAsync("No Un-Verified Partners Found.");
        }
    }
}
