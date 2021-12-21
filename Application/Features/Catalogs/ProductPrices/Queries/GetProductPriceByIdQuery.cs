using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductPrices.Queries
{
    public class GetProductPriceByIdQuery : IRequest<Result<ProductPriceResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetProductPriceByIdQueryHandler : IRequestHandler<GetProductPriceByIdQuery, Result<ProductPriceResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetProductPriceByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductPriceResponse>> Handle(GetProductPriceByIdQuery query, CancellationToken cancellationToken)
        {
            var productPriceInDb = await _unitOfWork.RepositoryFor<ProductPrice>().GetByIdAsync(query.Id);
            if (productPriceInDb != null && productPriceInDb.DeletedBy == null)
            {
                var mappedProductPrice = _mapper.Map<ProductPriceResponse>(productPriceInDb);
                return await Result<ProductPriceResponse>.SuccessAsync(mappedProductPrice);
            }
            return await Result<ProductPriceResponse>.FailAsync($"Product Price with Id: {query.Id} No Found.");
        }
    }
}
