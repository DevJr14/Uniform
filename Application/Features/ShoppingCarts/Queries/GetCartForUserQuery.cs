using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.ShoppingCarts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedR.Responses.ShoppingCart;
using SharedR.Wrapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts.Queries
{
    public class GetCartForUserQuery : IRequest<Result<ShoppingCartResponse>>
    {
        //Get shopping cart for current loggedIn user
    }
    internal class GetCartForUserQueryHandler : IRequestHandler<GetCartForUserQuery, Result<ShoppingCartResponse>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetCartForUserQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<ShoppingCartResponse>> Handle(GetCartForUserQuery request, CancellationToken cancellationToken)
        {
            var cartHeader = await _unitOfWork.RepositoryFor<CartHeader>().Entities
                .Where(ch => ch.CreatedBy == _currentUser.UserId)
                .FirstOrDefaultAsync();

            var cartDetails = await _unitOfWork.RepositoryFor<CartDetails>().Entities
                .Where(cd => cd.CartHeaderId == cartHeader.Id)
                .Include(cd => cd.Product)
                .ThenInclude(p => p.Images)
                .ToListAsync(cancellationToken: cancellationToken);

            ShoppingCart shoppingCart = new()
            {
                CartHeader = cartHeader,
                CartDetails = cartDetails
            };

            var mappedShoppingCard = _mapper.Map<ShoppingCartResponse>(shoppingCart);
            return await Result<ShoppingCartResponse>.SuccessAsync(mappedShoppingCard);
        }
    }
}
