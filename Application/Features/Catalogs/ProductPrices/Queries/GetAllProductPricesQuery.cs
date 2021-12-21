﻿using Application.Interfaces.Repositories;
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

namespace Application.Features.Catalogs.ProductPrices.Queries
{
    public class GetAllProductPricesQuery : IRequest<Result<List<ProductPriceResponse>>>
    {
    }

    internal class GetAllProductPricesQueryHandler : IRequestHandler<GetAllProductPricesQuery, Result<List<ProductPriceResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllProductPricesQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductPriceResponse>>> Handle(GetAllProductPricesQuery query, CancellationToken cancellationToken)
        {
            var productPricePrices = _unitOfWork.RepositoryFor<ProductPrice>().Entities
                .Where(p => p.DeletedBy == null)
                .ToList();
            if (productPricePrices.Count > 0)
            {
                var mappedProductPrices = _mapper.Map<List<ProductPriceResponse>>(productPricePrices);
                return await Result<List<ProductPriceResponse>>.SuccessAsync(mappedProductPrices);
            }
            return await Result<List<ProductPriceResponse>>.FailAsync("No Records Found.");
        }
    }
}
