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
    public class GetAllAddressesQuery : IRequest<Result<List<AddressResponse>>>
    {
    }

    internal class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, Result<List<AddressResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllAddressesQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<AddressResponse>>> Handle(GetAllAddressesQuery query, CancellationToken cancellationToken)
        {
            var addresses = await _unitOfWork.RepositoryFor<Address>().GetAllAsync();
            if (addresses.Where(a => a.DeletedBy == null).Count() > 0)
            {
                var mappedAddresses = _mapper.Map<List<AddressResponse>>(addresses.Where(a => a.DeletedBy == null));
                return await Result<List<AddressResponse>>.SuccessAsync(mappedAddresses);
            }
            return await Result<List<AddressResponse>>.FailAsync("No Records Found.");
        }
    }
}
