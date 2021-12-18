using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Responses.Partners;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Queries
{
    public class GetPartnerByIdQuery : IRequest<Result<PartnerResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetPartnerByIdQueryHandler : IRequestHandler<GetPartnerByIdQuery, Result<PartnerResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetPartnerByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PartnerResponse>> Handle(GetPartnerByIdQuery query, CancellationToken cancellationToken)
        {
            var partnerInDb = await _unitOfWork.RepositoryFor<Partner>().GetByIdAsync(query.Id);
            if(partnerInDb != null && partnerInDb.DeletedBy == null)
            {
                var mappedPartner = _mapper.Map<PartnerResponse>(partnerInDb);
                return await Result<PartnerResponse>.SuccessAsync(mappedPartner);
            }
            return await Result<PartnerResponse>.FailAsync($"Partner with Id: {query.Id} No Found.");
        }
    }
}
