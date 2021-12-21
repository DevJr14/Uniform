using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Catalogs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Result<CategoryResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetAllCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllCategoryByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.RepositoryFor<Category>().Entities
                .Where(c => c.Id == query.Id && c.DeletedBy == null)
                .FirstOrDefault();
            if (category != null)
            {
                var mappedCategory = _mapper.Map<CategoryResponse>(category);
                return await Result<CategoryResponse>.SuccessAsync(mappedCategory);
            }
            return await Result<CategoryResponse>.FailAsync($"Category with Id: {query.Id} No Found.");
        }
    }
}
