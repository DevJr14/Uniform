using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Partners;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Vehicles.Queries
{
    public class GetVehicleByIdQuery : IRequest<Result<VehicleResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Result<VehicleResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetVehicleByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<VehicleResponse>> Handle(GetVehicleByIdQuery query, CancellationToken cancellationToken)
        {
            var vehicleInDb = await _unitOfWork.RepositoryFor<Vehicle>().GetByIdAsync(query.Id);
            if (vehicleInDb != null && vehicleInDb.DeletedBy == null)
            {
                var mappedVehicle = _mapper.Map<VehicleResponse>(vehicleInDb);
                return await Result<VehicleResponse>.SuccessAsync(mappedVehicle);
            }
            return await Result<VehicleResponse>.FailAsync($"Vehicle with Id: {query.Id} No Found.");
        }
    }
}
