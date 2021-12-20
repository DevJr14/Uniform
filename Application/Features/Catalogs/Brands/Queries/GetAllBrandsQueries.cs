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
    public class GetAllBrandsQueries : IRequest<Result<List<BrandResponse>>>
    {
    }

    internal class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQueries, Result<List<BrandResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllBrandsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<BrandResponse>>> Handle(GetAllBrandsQueries query, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.RepositoryFor<Brand>().GetAllAsync();
            if (brands.Where(a => a.DeletedBy == null).Count() > 0)
            {
                var mappedBrands = _mapper.Map<List<BrandResponse>>(brands.Where(a => a.DeletedBy == null));
                return await Result<List<BrandResponse>>.SuccessAsync(mappedBrands);
            }
            return await Result<List<BrandResponse>>.FailAsync("No Records Found.");
        }
    }
}
