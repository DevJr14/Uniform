using Application.Features.Catalogs.Brands.Queries;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Tags.Queries
{
    public class GetAllTagsQuery : IRequest<Result<List<TagResponse>>>
    {
    }

    internal class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, Result<List<TagResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllTagsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<TagResponse>>> Handle(GetAllTagsQuery query, CancellationToken cancellationToken)
        {
            var tags = _unitOfWork.RepositoryFor<Tag>().Entities
                .Where(t => t.DeletedBy == null)
                .ToList();
            if (tags.Count > 0)
            {
                var mappedTags = _mapper.Map<List<TagResponse>>(tags);
                return await Result<List<TagResponse>>.SuccessAsync(mappedTags);
            }
            return await Result<List<TagResponse>>.FailAsync("No Records Found.");
        }
    }
}
