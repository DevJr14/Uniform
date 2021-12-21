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

namespace Application.Features.Catalogs.Categories.Queries
{
    public class GetAllCategoriesForPartnerQuery : IRequest<Result<List<CategoryResponse>>>
    {
    }

    internal class GetAllCategoriesForPartnerQueryHandler : IRequestHandler<GetAllCategoriesForPartnerQuery, Result<List<CategoryResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetAllCategoriesForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<List<CategoryResponse>>> Handle(GetAllCategoriesForPartnerQuery query, CancellationToken cancellationToken)
        {
            var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                          .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                          .FirstOrDefault();
            if (partner != null)
            {
                var categories = _unitOfWork.RepositoryFor<Category>().Entities
                    .Where(c => c.PartnerId ==partner.Id && c.DeletedBy == null)
                    .ToList();
                if (categories.Count > 0)
                {
                    var mappedCategories = _mapper.Map<List<CategoryResponse>>(categories.Where(a => a.DeletedBy == null));
                    return await Result<List<CategoryResponse>>.SuccessAsync(mappedCategories);
                }
                return await Result<List<CategoryResponse>>.FailAsync("No Records Found.");
            }
            return await Result<List<CategoryResponse>>.FailAsync("Partner Profile Not Verified.");
        }
    }
}
