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
    public class GetAllVehiclesForPartnerQuery : IRequest<Result<List<VehicleResponse>>>
    {
        public Guid PartnerId { get; set; }
    }

    internal class GetAllVehiclesForPartnerQueryHandler : IRequestHandler<GetAllVehiclesForPartnerQuery, Result<List<VehicleResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllVehiclesForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<VehicleResponse>>> Handle(GetAllVehiclesForPartnerQuery query, CancellationToken cancellationToken)
        {
            var vehicles = _unitOfWork.RepositoryFor<Vehicle>().Entities
                .Where(a => a.PartnerId == query.PartnerId && a.DeletedBy == null)
                .ToList();
            if (vehicles.Count > 0)
            {
                var mappedVehicles = _mapper.Map<List<VehicleResponse>>(vehicles);
                return await Result<List<VehicleResponse>>.SuccessAsync(mappedVehicles);
            }
            return await Result<List<VehicleResponse>>.FailAsync("No Records Found.");
        }
    }
}
