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
    public class GetAllPartnersQuery : IRequest<Result<List<PartnerResponse>>>
    {
    }

    internal class GetAllPartnersQueryHandler : IRequestHandler<GetAllPartnersQuery, Result<List<PartnerResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllPartnersQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<PartnerResponse>>> Handle(GetAllPartnersQuery query, CancellationToken cancellationToken)
        {
            var partners = await _unitOfWork.RepositoryFor<Partner>().GetAllAsync();
            if(partners.Where(p => p.DeletedBy == null).Count() > 0)
            {
                var mappedPartners = _mapper.Map<List<PartnerResponse>>(partners.Where(p => p.DeletedBy == null));
                return await Result<List<PartnerResponse>>.SuccessAsync(mappedPartners);
            }
            return await Result<List<PartnerResponse>>.FailAsync("No Records Found.");
        }
    }
}
