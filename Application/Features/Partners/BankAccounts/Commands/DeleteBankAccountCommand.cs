using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.BankAccounts.Commands
{
    public class DeleteBankAccountCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteBankAccountCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteBankAccountCommand command, CancellationToken cancellationToken)
        {
            var bankAccountInDb = await _unitOfWork.RepositoryFor<BankAccount>().GetByIdAsync(command.Id);
            if (bankAccountInDb != null && bankAccountInDb.DeletedBy == null)
            {
                bankAccountInDb.DeletedBy = _currentUser.UserId;
                bankAccountInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<BankAccount>().MarkDeletedAsync(bankAccountInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(bankAccountInDb.Id, "Bank Account Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Bank Account with Id: {command.Id} Not Found.");
        }
    }
}
