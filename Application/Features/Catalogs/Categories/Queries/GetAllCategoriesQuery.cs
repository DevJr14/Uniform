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

namespace Application.Features.Catalogs.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<Result<List<CategoryResponse>>>
    {
    }

    internal class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllCategoriesQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<CategoryResponse>>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.RepositoryFor<Category>().GetAllAsync();
            if (categories.Where(a => a.DeletedBy == null).Count() > 0)
            {
                var mappedCategories = _mapper.Map<List<CategoryResponse>>(categories.Where(a => a.DeletedBy == null));
                return await Result<List<CategoryResponse>>.SuccessAsync(mappedCategories);
            }
            return await Result<List<CategoryResponse>>.FailAsync("No Records Found.");
        }
    }
}
