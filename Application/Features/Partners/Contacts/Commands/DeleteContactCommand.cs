using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Contacts.Commands
{
    public class DeleteContactCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteContactCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
        {
            var contactInDb = await _unitOfWork.RepositoryFor<Contact>().GetByIdAsync(command.Id);
            if (contactInDb != null && contactInDb.DeletedBy == null)
            {
                contactInDb.DeletedBy = _currentUser.UserId;
                contactInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Contact>().MarkDeletedAsync(contactInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(contactInDb.Id, "Contact Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Contact with Id: {command.Id} Not Found.");
        }
    }
}
