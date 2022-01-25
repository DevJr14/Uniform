using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.ShoppingCarts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedR.Requests.ShoppingCart;
using SharedR.Wrapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts.Commands
{
    public class AddEditCartCommand : IRequest<Result<Guid>>
    {
        public ShoppingCartRequest CartRequest { get; set; }
    }

    internal class AddEditCardCommandHandler : IRequestHandler<AddEditCartCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;

        public AddEditCardCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddEditCartCommand request, CancellationToken cancellationToken)
        {
            var cart = _mapper.Map<ShoppingCart>(request.CartRequest);

            if (request.CartRequest.CartHeader.CartHederId == Guid.Empty)
            {
                //Add New Header
                await _unitOfWork.RepositoryFor<CartHeader>().AddAsync(cart.CartHeader);
                await _unitOfWork.Commit(cancellationToken);

                //New Details
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                await _unitOfWork.RepositoryFor<CartDetails>().AddAsync(cart.CartDetails.FirstOrDefault());
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(cart.CartHeader.Id, "Shopping Cart Added Successfully.");
            }
            else
            {
                //Update Existing
                var cartHeaderInDb = await _unitOfWork.RepositoryFor<CartHeader>().GetByIdAsync(request.CartRequest.CartHeader.CartHederId);

                if (cartHeaderInDb != null)
                {
                    var cartDetailsInDb = await _unitOfWork.RepositoryFor<CartDetails>()
                        .Entities.Where(cd => cd.ProductId == request.CartRequest.CartDetails.FirstOrDefault().ProductId
                        && cd.CartHeaderId == cartHeaderInDb.Id)
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    if (cartDetailsInDb == null)
                    {
                        //Add New Details
                        cart.CartDetails.FirstOrDefault().CartHeaderId = request.CartRequest.CartHeader.CartHederId;
                        await _unitOfWork.RepositoryFor<CartDetails>().AddAsync(cart.CartDetails.FirstOrDefault());
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<Guid>.SuccessAsync(request.CartRequest.CartHeader.CartHederId, "Product Added To Shopping Cart Successfully.");
                    }
                    else
                    {
                        //Updated (increase card qty)
                        cartDetailsInDb.Count += cart.CartDetails.FirstOrDefault().Count;

                        await _unitOfWork.RepositoryFor<CartDetails>().UpdateAsync(cartDetailsInDb);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<Guid>.SuccessAsync(cartDetailsInDb.CartHeaderId, "Shopping Cart Updated Successfully.");
                    }
                }
                else
                {
                    return await Result<Guid>.FailAsync($"Shopping Cart with Header Id: {request.CartRequest.CartHeader.CartHederId} Does not exists.");
                }
            }
        }
    }
}
