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

namespace Application.Features.Catalogs.ProductTags.Queries
{
    public class GetAllProductTagsQuery : IRequest<Result<List<ProductTagsResponse>>>
    {
        public Guid ProductId { get; set; }
    }

    internal class GetAllProductTagsQueryHandler : IRequestHandler<GetAllProductTagsQuery, Result<List<ProductTagsResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllProductTagsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductTagsResponse>>> Handle(GetAllProductTagsQuery query, CancellationToken cancellationToken)
        {
            var prodTags = _unitOfWork.RepositoryFor<ProductCategory>().Entities
                .Where(pc => pc.ProductId == query.ProductId)
                .ToList();
            if (prodTags.Count > 0)
            {
                var mappedProdTags = _mapper.Map<List<ProductTagsResponse>>(prodTags);
                return await Result<List<ProductTagsResponse>>.SuccessAsync(mappedProdTags);
            }
            return await Result<List<ProductTagsResponse>>.FailAsync($"Product With Id: {query.ProductId} Has No Tags.");
        }
    }
}
