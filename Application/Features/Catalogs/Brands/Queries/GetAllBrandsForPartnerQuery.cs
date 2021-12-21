using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Brands.Queries
{
    public class GetAllBrandsForPartnerQuery : IRequest<Result<List<BrandResponse>>>
    {
        public Guid PartnerId { get; set; }
    }

    internal class GetAllBrandsForPartnerQueryHandler : IRequestHandler<GetAllBrandsForPartnerQuery, Result<List<BrandResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllBrandsForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<BrandResponse>>> Handle(GetAllBrandsForPartnerQuery query, CancellationToken cancellationToken)
        {
            var brands = _unitOfWork.RepositoryFor<Brand>().Entities
                .Where(a => a.PartnerId == query.PartnerId && a.DeletedBy == null)
                .ToList();
            if (brands.Count > 0)
            {
                var mappedBrands = _mapper.Map<List<BrandResponse>>(brands);
                return await Result<List<BrandResponse>>.SuccessAsync(mappedBrands);
            }
            return await Result<List<BrandResponse>>.FailAsync("No Records Found.");
        }
    }
}
