using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Brands.Queries
{
    public class GetBrandByIdQuery : IRequest<Result<BrandResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<BrandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetBrandByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<BrandResponse>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
        {
            var brandInDb = await _unitOfWork.RepositoryFor<Brand>().GetByIdAsync(query.Id);
            if (brandInDb != null && brandInDb.DeletedBy == null)
            {
                var mappedBrand = _mapper.Map<BrandResponse>(brandInDb);
                return await Result<BrandResponse>.SuccessAsync(mappedBrand);
            }
            return await Result<BrandResponse>.FailAsync($"Brand with Id: {query.Id} No Found.");
        }
    }
}
