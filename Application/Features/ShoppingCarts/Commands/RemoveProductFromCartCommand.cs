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
    public class RemoveProductFromCartCommand : IRequest<Result<Guid>>
    {
        //Remove single product from cart
        public Guid CardDetailsId { get; set; }
    }
    internal class RemoveProductFromCartCommandHandler : IRequestHandler<RemoveProductFromCartCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public RemoveProductFromCartCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
        {
            var cartDetailsInDb = await _unitOfWork.RepositoryFor<CartDetails>().GetByIdAsync(request.CardDetailsId);
            if (cartDetailsInDb == null)
            {
                return await Result<Guid>.FailAsync($"Shopping Cart Details with Id: {request.CardDetailsId} does not exists.");
            }
            //If you are removing the last item within a shopping cart, also remove its header
            var totalCartDetails = await _unitOfWork.RepositoryFor<CartDetails>().Entities
                .Where(cd => cd.CartHeaderId == cartDetailsInDb.CartHeaderId)
                .CountAsync();

            cartDetailsInDb.DeletedBy = _currentUser.UserId;
            cartDetailsInDb.DeletedOn = DateTime.Now;
            await _unitOfWork.RepositoryFor<CartDetails>().MarkDeletedAsync(cartDetailsInDb);
            if (totalCartDetails == 1)
            {
                var cartHeaderInDb = await _unitOfWork.RepositoryFor<CartHeader>().GetByIdAsync(cartDetailsInDb.CartHeaderId);
                cartHeaderInDb.DeletedBy = _currentUser.UserId;
                cartHeaderInDb.DeletedOn = DateTime.Now;
                await _unitOfWork.RepositoryFor<CartHeader>().MarkDeletedAsync(cartHeaderInDb);
            }
            await _unitOfWork.Commit(cancellationToken);

            return await Result<Guid>.SuccessAsync(cartDetailsInDb.Id, "Shopping Cart Details Removed Successfully.");
        }
    }
}
