using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Brands.Queries
{
    public class GetAllBrandsQuery : IRequest<Result<List<BrandResponse>>>
    {
    }

    internal class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, Result<List<BrandResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllBrandsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<BrandResponse>>> Handle(GetAllBrandsQuery query, CancellationToken cancellationToken)
        {
            var brands = _unitOfWork.RepositoryFor<Brand>().Entities
                .Where(b => b.DeletedBy == null)
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
