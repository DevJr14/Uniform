using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteCategoryCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var categoryInDb = await _unitOfWork.RepositoryFor<Category>().GetByIdAsync(command.Id);
            if (categoryInDb != null && categoryInDb.DeletedBy == null)
            {
                categoryInDb.DeletedBy = _currentUser.UserId;
                categoryInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Category>().MarkDeletedAsync(categoryInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(categoryInDb.Id, "Category Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Category with Id: {command.Id} Not Found.");
        }
    }
}
