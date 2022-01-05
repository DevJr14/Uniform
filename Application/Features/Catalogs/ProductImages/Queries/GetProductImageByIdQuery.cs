using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedR.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductImages.Queries
{
    public class GetProductImageByIdQuery : IRequest<Result<ProductImageResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetProductImageByIdQueryHandler : IRequestHandler<GetProductImageByIdQuery, Result<ProductImageResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetProductImageByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductImageResponse>> Handle(GetProductImageByIdQuery query, CancellationToken cancellationToken)
        {
            var productImageInDb = await _unitOfWork.RepositoryFor<ProductImage>().Entities
                .Where(pi => pi.Id == query.Id && pi.DeletedBy == null)
                .FirstOrDefaultAsync();

            if (productImageInDb != null)
            {
                var mappedProductImage = _mapper.Map<ProductImageResponse>(productImageInDb);
                return await Result<ProductImageResponse>.SuccessAsync(mappedProductImage);
            }
            return await Result<ProductImageResponse>.FailAsync($"Product Image with Id: {query.Id} not found.");
        }
    }
}
