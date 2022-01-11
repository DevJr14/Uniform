using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Requests.Partners;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Vehicles.Commands
{
    public class AddEditVehicleCommand : IRequest<Result<Guid>>
    {
        public VehicleRequest VehicleRequest { get; set; }
    }

    internal class AddEditVehicleCommandHandler : IRequestHandler<AddEditVehicleCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public AddEditVehicleCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditVehicleCommand command, CancellationToken cancellationToken)
        {
            if (command.VehicleRequest.Id == Guid.Empty)
            {
                var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                    .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.Id == command.VehicleRequest.PartnerId)
                    .FirstOrDefault();
                if (partner != null)
                {
                    var vehicle = _mapper.Map<Vehicle>(command.VehicleRequest);
                    vehicle.PartnerId = partner.Id;
                    await _unitOfWork.RepositoryFor<Vehicle>().AddAsync(vehicle);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(vehicle.Id, "Vehicle Saved Successfully.");
                }
                return await Result<Guid>.FailAsync("Partner Profile Not Verified.");
            }
            else
            {
                var vehicleInDb = await _unitOfWork.RepositoryFor<Vehicle>().GetByIdAsync(command.VehicleRequest.Id);
                if (vehicleInDb != null && vehicleInDb.DeletedBy == null)
                {
                    vehicleInDb.Name = command.VehicleRequest.Name ?? vehicleInDb.Name;
                    vehicleInDb.Make = command.VehicleRequest.Make ?? vehicleInDb.Make;
                    vehicleInDb.Model = command.VehicleRequest.Model ?? vehicleInDb.Model;
                    vehicleInDb.Colour = command.VehicleRequest.Colour ?? vehicleInDb.Colour;
                    vehicleInDb.LicensePlate = command.VehicleRequest.LicensePlate ?? vehicleInDb.LicensePlate;
                    vehicleInDb.Description = command.VehicleRequest.Description ?? vehicleInDb.Description;
                    vehicleInDb.VINNo = command.VehicleRequest.VINNo ?? vehicleInDb.VINNo;
                    vehicleInDb.Mileage = command.VehicleRequest.Mileage;
                    vehicleInDb.LicenseExpiry = command.VehicleRequest.LicenseExpiry;

                    await _unitOfWork.RepositoryFor<Vehicle>().UpdateAsync(vehicleInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(vehicleInDb.Id, "Vehicle Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Vehicle Not Found.");
            }
        }
    }
}
