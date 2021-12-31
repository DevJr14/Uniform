using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductImages.Queries
{
    public class GetProductImagesByProductIdQuery : IRequest<Result<List<ProductImageResponse>>>
    {
        public Guid ProductId { get; set; }
    }

    internal class GetProductImagesByProductIdQueryHandler : IRequestHandler<GetProductImagesByProductIdQuery, Result<List<ProductImageResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetProductImagesByProductIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductImageResponse>>> Handle(GetProductImagesByProductIdQuery query, CancellationToken cancellationToken)
        {
            var productImagesInDb = await _unitOfWork.RepositoryFor<ProductImage>().Entities
                .Where(pi => pi.ProductId == query.ProductId && pi.DeletedBy == null)
                .ToListAsync();

            if (productImagesInDb.Any())
            {
                var mappedProductImages = _mapper.Map<List<ProductImageResponse>>(productImagesInDb);
                return await Result<List<ProductImageResponse>>.SuccessAsync(mappedProductImages);
            }
            return await Result<List<ProductImageResponse>>.FailAsync($"Product with Id: {query.ProductId} has no price.");
        }
    }
}
