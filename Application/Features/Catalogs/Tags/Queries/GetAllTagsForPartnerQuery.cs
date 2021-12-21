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

namespace Application.Features.Catalogs.Tags.Queries
{
    public class GetAllTagsForPartnerQuery : IRequest<Result<List<TagResponse>>>
    {
    }

    internal class GetAllTagsForPartnerQueryHandler : IRequestHandler<GetAllTagsForPartnerQuery, Result<List<TagResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetAllTagsForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<List<TagResponse>>> Handle(GetAllTagsForPartnerQuery query, CancellationToken cancellationToken)
        {
            var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                       .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                       .FirstOrDefault();
            if (partner != null)
            {
                var tags = _unitOfWork.RepositoryFor<Tag>().Entities
                    .Where(t => t.PartnerId == partner.Id && t.DeletedBy == null)
                    .ToList();
                if (tags.Count > 0)
                {
                    var mappedTags = _mapper.Map<List<TagResponse>>(tags);
                    return await Result<List<TagResponse>>.SuccessAsync(mappedTags);
                }
                return await Result<List<TagResponse>>.FailAsync("No Records Found.");
            }
            return await Result<List<TagResponse>>.FailAsync("Partner Profile Not Verified.");
        }
    }
}
