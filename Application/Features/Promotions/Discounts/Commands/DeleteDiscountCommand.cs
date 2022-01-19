using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Promotions;
using MediatR;
using SharedR.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Promotions.Discounts.Commands
{
    public class DeleteDiscountCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteDiscountCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteDiscountCommand command, CancellationToken cancellationToken)
        {
            var discountInDb = await _unitOfWork.RepositoryFor<Discount>().GetByIdAsync(command.Id);
            if (discountInDb != null && discountInDb.DeletedBy == null)
            {
                discountInDb.DeletedBy = _currentUser.UserId;
                discountInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Discount>().MarkDeletedAsync(discountInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(discountInDb.Id, "Discount Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Discount with Id: {command.Id} Not Found.");
        }
    }
}
