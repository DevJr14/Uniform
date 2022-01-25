using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.ShoppingCarts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedR.Wrapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts.Commands
{
    public class DeleteCartCommand : IRequest<Result<Guid>>
    {
        //Current LoggedIn user's cart to be cleared
    }

    internal class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteCartCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var cardHeaderInDb = await _unitOfWork.RepositoryFor<CartHeader>().Entities
                .Where(ch => ch.CreatedBy == _currentUser.UserId)
                .FirstOrDefaultAsync();
            if (cardHeaderInDb == null)
            {
                return await Result<Guid>.FailAsync($"User has no shopping cart.");
            }
            var cartDetailsToRemove = await _unitOfWork.RepositoryFor<CartDetails>().Entities
                .Where(cd => cd.CartHeaderId == cardHeaderInDb.Id)
                .ToListAsync();
            //Delete products from cart details
            await _unitOfWork.RepositoryFor<CartDetails>().MarkDeletedRangeAsync(cartDetailsToRemove);
            //Delete cart header
            await _unitOfWork.RepositoryFor<CartHeader>().MarkDeletedAsync(cardHeaderInDb);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<Guid>.SuccessAsync(cardHeaderInDb.Id, "Shopping Cart Deleted Successfully.");
        }
    }
}
