using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Brands.Commands
{
    public class DeleteBrandCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteBrandCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteBrandCommand command, CancellationToken cancellationToken)
        {
            var brandInDb = await _unitOfWork.RepositoryFor<Brand>().GetByIdAsync(command.Id);
            if (brandInDb != null && brandInDb.DeletedBy == null)
            {
                brandInDb.DeletedBy = _currentUser.UserId;
                brandInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Brand>().MarkDeletedAsync(brandInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(brandInDb.Id, "Brand Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Brand with Id: {command.Id} Not Found.");
        }
    }
}
