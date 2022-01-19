using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using Domain.Entities.Promotions;
using MediatR;
using SharedR.Requests.Promotions;
using SharedR.Wrapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Promotions.Discounts.Commands
{
    public class AddEditDiscountCommand : IRequest<Result<Guid>>
    {
        public DiscountRequest DiscountRequest { get; set; }
    }

    internal class AddEditDiscountCommandHandler : IRequestHandler<AddEditDiscountCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public AddEditDiscountCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditDiscountCommand command, CancellationToken cancellationToken)
        {
            if (command.DiscountRequest.Id == Guid.Empty)
            {
                var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                    .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                    .FirstOrDefault();
                if (partner != null)
                {
                    var discount = _mapper.Map<Discount>(command.DiscountRequest);
                    discount.PartnerId = partner.Id;
                    await _unitOfWork.RepositoryFor<Discount>().AddAsync(discount);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(discount.Id, "Discount Saved Successfully.");
                }
                return await Result<Guid>.FailAsync("Partner Profile Not Verified.");
            }
            else
            {
                var discountInDb = await _unitOfWork.RepositoryFor<Discount>().GetByIdAsync(command.DiscountRequest.Id);
                if (discountInDb != null && discountInDb.DeletedBy == null)
                {
                    discountInDb.Name = command.DiscountRequest.Name ?? discountInDb.Name;
                    discountInDb.Percentage = command.DiscountRequest.Percentage;
                    discountInDb.StartDate = command.DiscountRequest.StartDate;
                    discountInDb.EndDate = command.DiscountRequest.EndDate;

                    await _unitOfWork.RepositoryFor<Discount>().UpdateAsync(discountInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(discountInDb.Id, "Discount Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Discount Not Found.");
            }
        }
    }
}
