using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Addresses.Queries
{
    public class GetAllAddressesForPartnerQuery : IRequest<Result<List<AddressResponse>>>
    {
        public Guid PartnerId { get; set; }
    }

    internal class GetAllAddressesForPartnerQueryHandler : IRequestHandler<GetAllAddressesForPartnerQuery, Result<List<AddressResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllAddressesForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<AddressResponse>>> Handle(GetAllAddressesForPartnerQuery query, CancellationToken cancellationToken)
        {
            var addresses = _unitOfWork.RepositoryFor<Address>().Entities
                .Where(a => a.PartnerId == query.PartnerId && a.DeletedBy == null)
                .ToList();
            if (addresses.Count > 0)
            {
                var mappedAddresses = _mapper.Map<List<AddressResponse>>(addresses);
                return await Result<List<AddressResponse>>.SuccessAsync(mappedAddresses);
            }
            return await Result<List<AddressResponse>>.FailAsync("No Records Found.");
        }
    }
}
