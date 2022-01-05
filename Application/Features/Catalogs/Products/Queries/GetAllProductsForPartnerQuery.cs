using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using Domain.Entities.Partners;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedR.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Products.Queries
{
    public class GetAllProductsForPartnerQuery : IRequest<Result<List<ProductResponse>>>
    {
    }

    internal class GetAllProductsForPartnerQueryHandler : IRequestHandler<GetAllProductsForPartnerQuery, Result<List<ProductResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetAllProductsForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<List<ProductResponse>>> Handle(GetAllProductsForPartnerQuery query, CancellationToken cancellationToken)
        {
            var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                          .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                          .FirstOrDefault();
            if (partner != null)
            {
                var products = _unitOfWork.RepositoryFor<Product>().Entities                    
                    .Where(a => a.PartnerId == partner.Id && a.DeletedBy == null)
                    .Include(p => p.Partner)
                    .Include(p => p.Brand)
                    .ToList();
                if (products.Count > 0)
                {
                    var mappedBrands = _mapper.Map<List<ProductResponse>>(products);
                    return await Result<List<ProductResponse>>.SuccessAsync(mappedBrands);
                }
                return await Result<List<ProductResponse>>.FailAsync("No Records Found.");
            }
            return await Result<List<ProductResponse>>.FailAsync("Partner Profile Not Verified.");
        }
    }
}
