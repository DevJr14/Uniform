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

namespace Application.Features.Catalogs.Products.Queries
{
    public class GetAllProductsQuery : IRequest<Result<List<ProductResponse>>>
    {
    }

    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<ProductResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductResponse>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = _unitOfWork.RepositoryFor<Product>().Entities
                .Where(p => p.DeletedBy == null)
                .ToList();
            if (products.Count > 0)
            {
                var mappedProducts = _mapper.Map<List<ProductResponse>>(products);
                return await Result<List<ProductResponse>>.SuccessAsync(mappedProducts);
            }
            return await Result<List<ProductResponse>>.FailAsync("No Records Found.");
        }
    }
}
