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

namespace Application.Features.Catalogs.ProductCategories.Queries
{
    public class GetAllProductCategoriesQuery : IRequest<Result<List<ProductCategoriesResponse>>>
    {
        public Guid ProductId { get; set; }
    }

    internal class GetAllProductCategoriesQueryHandler : IRequestHandler<GetAllProductCategoriesQuery, Result<List<ProductCategoriesResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllProductCategoriesQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductCategoriesResponse>>> Handle(GetAllProductCategoriesQuery query, CancellationToken cancellationToken)
        {
            var prodCategories = _unitOfWork.RepositoryFor<ProductCategory>().Entities
                .Where(pc => pc.ProductId == query.ProductId)
                .ToList();
            if (prodCategories.Count > 0)
            {
                var mappedProdCategories = _mapper.Map<List<ProductCategoriesResponse>>(prodCategories);
                return await Result<List<ProductCategoriesResponse>>.SuccessAsync(mappedProdCategories);
            }
            return await Result<List<ProductCategoriesResponse>>.FailAsync($"Product With Id: {query.ProductId} Has No Categories.");
        }
    }
}
