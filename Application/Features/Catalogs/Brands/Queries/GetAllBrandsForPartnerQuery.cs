using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using Domain.Entities.Partners;
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
    }

    internal class GetAllBrandsForPartnerQueryHandler : IRequestHandler<GetAllBrandsForPartnerQuery, Result<List<BrandResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetAllBrandsForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<List<BrandResponse>>> Handle(GetAllBrandsForPartnerQuery query, CancellationToken cancellationToken)
        {
            var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                          .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                          .FirstOrDefault();
            if (partner != null)
            {
                var brands = _unitOfWork.RepositoryFor<Brand>().Entities
                    .Where(a => a.PartnerId == partner.Id && a.DeletedBy == null)
                    .ToList();
                if (brands.Count > 0)
                {
                    var mappedBrands = _mapper.Map<List<BrandResponse>>(brands);
                    return await Result<List<BrandResponse>>.SuccessAsync(mappedBrands);
                }
                return await Result<List<BrandResponse>>.FailAsync("No Records Found.");
            }
            return await Result<List<BrandResponse>>.FailAsync("Partner Profile Not Verified.");
        }
    }
}
