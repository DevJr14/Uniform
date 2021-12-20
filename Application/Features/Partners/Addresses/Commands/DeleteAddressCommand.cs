using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Addresses.Commands
{
    public class DeleteAddressCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteAddressCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
        {
            var addresInDb = await _unitOfWork.RepositoryFor<Address>().GetByIdAsync(command.Id);
            if (addresInDb != null && addresInDb.DeletedBy == null)
            {
                addresInDb.DeletedBy = _currentUser.UserId;
                addresInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Address>().MarkDeletedAsync(addresInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(addresInDb.Id, "Address Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Address with Id: {command.Id} Not Found.");
        }
    }
}
