using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Vehicles.Queries
{
    public class GetAllVehiclesQuery : IRequest<Result<List<VehicleResponse>>>
    {
    }

    internal class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, Result<List<VehicleResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllVehiclesQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<VehicleResponse>>> Handle(GetAllVehiclesQuery query, CancellationToken cancellationToken)
        {
            var vehicles = await _unitOfWork.RepositoryFor<Vehicle>().GetAllAsync();
            if (vehicles.Where(a => a.DeletedBy == null).Count() > 0)
            {
                var mappedVehicles = _mapper.Map<List<VehicleResponse>>(vehicles.Where(a => a.DeletedBy == null));
                return await Result<List<VehicleResponse>>.SuccessAsync(mappedVehicles);
            }
            return await Result<List<VehicleResponse>>.FailAsync("No Records Found.");
        }
    }
}
