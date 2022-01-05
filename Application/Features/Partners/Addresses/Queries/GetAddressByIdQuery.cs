using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Responses.Partners;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Addresses.Queries
{
    public class GetAddressByIdQuery : IRequest<Result<AddressResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, Result<AddressResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAddressByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddressResponse>> Handle(GetAddressByIdQuery query, CancellationToken cancellationToken)
        {
            var addressInDb = await _unitOfWork.RepositoryFor<Address>().GetByIdAsync(query.Id);
            if (addressInDb != null && addressInDb.DeletedBy == null)
            {
                var mappedAddress = _mapper.Map<AddressResponse>(addressInDb);
                return await Result<AddressResponse>.SuccessAsync(mappedAddress);
            }
            return await Result<AddressResponse>.FailAsync($"Address with Id: {query.Id} No Found.");
        }
    }
}
