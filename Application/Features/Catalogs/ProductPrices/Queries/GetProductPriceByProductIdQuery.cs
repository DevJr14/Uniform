using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductPrices.Queries
{
    public class GetProductPriceByProductIdQuery : IRequest<Result<ProductPriceResponse>>
    {
        public Guid ProductId { get; set; }
    }

    internal class GetProductPriceByProductIdQueryHandler : IRequestHandler<GetProductPriceByProductIdQuery, Result<ProductPriceResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetProductPriceByProductIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductPriceResponse>> Handle(GetProductPriceByProductIdQuery query, CancellationToken cancellationToken)
        {
            var productPriceInDb = await _unitOfWork.RepositoryFor<ProductPrice>().Entities
                .Where(pp => pp.ProductId == query.ProductId && pp.DeletedBy == null)
                .Include(pp => pp.Discount)
                .FirstOrDefaultAsync();

            if (productPriceInDb != null)
            {
                var mappedProductPrice = _mapper.Map<ProductPriceResponse>(productPriceInDb);
                return await Result<ProductPriceResponse>.SuccessAsync(mappedProductPrice);
            }
            return await Result<ProductPriceResponse>.FailAsync($"Product with Id: {query.ProductId} has no price.");
        }
    }
}
