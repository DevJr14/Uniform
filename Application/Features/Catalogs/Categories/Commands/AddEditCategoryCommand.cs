using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Categories.Commands
{
    public class AddEditCategoryCommand : IRequest<Result<Guid>>
    {
        public CategoryRequest CategoryRequest { get; set; }
    }

    internal class AddEditCategoryCommandHandler : IRequestHandler<AddEditCategoryCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public AddEditCategoryCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditCategoryCommand command, CancellationToken cancellationToken)
        {
            if (command.CategoryRequest.Id == Guid.Empty)
            {
                var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                    .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                    .FirstOrDefault();
                if (partner != null)
                {
                    var category = _mapper.Map<Category>(command.CategoryRequest);
                    category.PartnerId = partner.Id;
                    await _unitOfWork.RepositoryFor<Category>().AddAsync(category);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(category.Id, "Category Saved Successfully.");
                }
                return await Result<Guid>.FailAsync("Partner Profile Not Verified.");
            }
            else
            {
                var categoryInDb = await _unitOfWork.RepositoryFor<Category>().GetByIdAsync(command.CategoryRequest.Id);
                if (categoryInDb != null && categoryInDb.DeletedBy == null)
                {
                    categoryInDb.Name = command.CategoryRequest.Name ?? categoryInDb.Name;
                    categoryInDb.Description = command.CategoryRequest.Description ?? categoryInDb.Description;
                    categoryInDb.IsActive = command.CategoryRequest.IsActive;

                    await _unitOfWork.RepositoryFor<Category>().UpdateAsync(categoryInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(categoryInDb.Id, "Category Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Category Not Found.");
            }
        }
    }
}
