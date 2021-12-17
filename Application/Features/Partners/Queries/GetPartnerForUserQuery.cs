using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using Shared.Responses.Partners;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Queries
{
    public class GetPartnerForUserQuery : IRequest<Result<PartnerResponse>>
    {
        public Guid UserId { get; set; }
    }

    internal class GetPartnerForUserQueryHandler : IRequestHandler<GetPartnerForUserQuery, Result<PartnerResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetPartnerForUserQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<PartnerResponse>> Handle(GetPartnerForUserQuery query, CancellationToken cancellationToken)
        {
            if(query.UserId == Guid.Empty)
            {
                var partnerInDb = _unitOfWork.RepositoryFor<Partner>()
                    .Entities
                    .Where(p => p.UserId.ToString().ToUpper() == _currentUser.UserId.ToUpper() && p.DeletedBy == null)
                    .FirstOrDefault();
                if (partnerInDb != null)
                {
                    var mappedPartner = _mapper.Map<PartnerResponse>(partnerInDb);
                    return await Result<PartnerResponse>.SuccessAsync(mappedPartner);
                }
                return await Result<PartnerResponse>.FailAsync($"No Partnership Found For User with Id: {query.UserId}.");
            }
            else
            {
                var partnerInDb = _unitOfWork.RepositoryFor<Partner>()
                    .Entities
                    .Where(p => p.UserId == query.UserId && p.DeletedBy == null)
                    .FirstOrDefault();
                if(partnerInDb != null)
                {
                    var mappedPartner = _mapper.Map<PartnerResponse>(partnerInDb);
                    return await Result<PartnerResponse>.SuccessAsync(mappedPartner);
                }
                return await Result<PartnerResponse>.FailAsync($"No Partnership Found For User with Id: {query.UserId}.");
            }
        }
    }
}
