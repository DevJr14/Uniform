using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Partners;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Addresses.Commands
{
    public class AddEditAddressCommand : IRequest<Result<Guid>>
    {
        public AddressRequest AddressRequest { get; set; }
    }

    internal class AddEditAddressCommandHandler : IRequestHandler<AddEditAddressCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        public AddEditAddressCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddEditAddressCommand command, CancellationToken cancellationToken)
        {
            if(command.AddressRequest.Id == Guid.Empty)
            {
                var address = _mapper.Map<Address>(command.AddressRequest);
                await _unitOfWork.RepositoryFor<Address>().AddAsync(address);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(address.Id, "Address Saved Successfully.");
            }
            else
            {
                var addressInDb = await _unitOfWork.RepositoryFor<Address>().GetByIdAsync(command.AddressRequest.Id);
                if(addressInDb != null && addressInDb.DeletedBy == null)
                {
                    addressInDb.StreetName = command.AddressRequest.StreetName ?? addressInDb.StreetName;
                    addressInDb.Suburb = command.AddressRequest.Suburb ?? addressInDb.Suburb;
                    addressInDb.City = command.AddressRequest.City ?? addressInDb.City;
                    addressInDb.Province = command.AddressRequest.Province ?? addressInDb.Province;
                    addressInDb.Country = command.AddressRequest.Country ?? addressInDb.Country;
                    addressInDb.PostalCode = command.AddressRequest.PostalCode ?? addressInDb.PostalCode;
                    addressInDb.IsActive = command.AddressRequest.IsActive;//To Do: Ensure only One Address is Active

                    await _unitOfWork.RepositoryFor<Address>().UpdateAsync(addressInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(addressInDb.Id, "Address Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Address Not Found.");
            }
        }
    }
}
