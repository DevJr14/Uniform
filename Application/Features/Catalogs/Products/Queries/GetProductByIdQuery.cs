using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Result<ProductResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var productInDb = await _unitOfWork.RepositoryFor<Product>().GetByIdAsync(query.Id);
            if (productInDb != null && productInDb.DeletedBy == null)
            {
                var mappedProduct = _mapper.Map<ProductResponse>(productInDb);
                return await Result<ProductResponse>.SuccessAsync(mappedProduct);
            }
            return await Result<ProductResponse>.FailAsync($"Product with Id: {query.Id} No Found.");
        }
    }
}
