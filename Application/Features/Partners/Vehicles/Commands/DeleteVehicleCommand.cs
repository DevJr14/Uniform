using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Vehicles.Commands
{
    public class DeleteVehicleCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteVehicleCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteVehicleCommand command, CancellationToken cancellationToken)
        {
            var vehicleInDb = await _unitOfWork.RepositoryFor<Vehicle>().GetByIdAsync(command.Id);
            if (vehicleInDb != null && vehicleInDb.DeletedBy == null)
            {
                vehicleInDb.DeletedBy = _currentUser.UserId;
                vehicleInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Vehicle>().MarkDeletedAsync(vehicleInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(vehicleInDb.Id, "Vehicle Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Vehicle with Id: {command.Id} Not Found.");
        }
    }
}
