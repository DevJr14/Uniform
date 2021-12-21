using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Tags.Queries
{
    public class GetTagByIdQuery : IRequest<Result<TagResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, Result<TagResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetTagByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TagResponse>> Handle(GetTagByIdQuery query, CancellationToken cancellationToken)
        {
            var tag = _unitOfWork.RepositoryFor<Tag>().Entities
                .Where(t => t.Id == query.Id && t.DeletedBy == null)
                .FirstOrDefault();
            if (tag != null)
            {
                var mappedTag = _mapper.Map<TagResponse>(tag);
                return await Result<TagResponse>.SuccessAsync(mappedTag);
            }
            return await Result<TagResponse>.FailAsync($"Tag with Id: {query.Id} No Found.");
        }
    }
}
