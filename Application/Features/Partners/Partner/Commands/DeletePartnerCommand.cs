using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Commands
{
    public class DeletePartnerCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeletePartnerCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeletePartnerCommand command, CancellationToken cancellationToken)
        {
            var partnerInDb = await _unitOfWork.RepositoryFor<Partner>().GetByIdAsync(command.Id);
            if (partnerInDb != null && partnerInDb.DeletedBy == null)
            {
                partnerInDb.DeletedBy = _currentUser.UserId;
                partnerInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Partner>().MarkDeletedAsync(partnerInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(partnerInDb.Id, "Partner Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Partner with Id: {command.Id} Not Found.");
        }
    }
}
