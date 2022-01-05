using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Requests.Partners;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.BankAccounts.Commands
{
    public class AddEditBankAccountCommand : IRequest<Result<Guid>>
    {
        public BankAccountRequest BankAccountRequest { get; set; }
    }

    internal class AddEditBankAccountCommandHandler : IRequestHandler<AddEditBankAccountCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public AddEditBankAccountCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditBankAccountCommand command, CancellationToken cancellationToken)
        {
            if (command.BankAccountRequest.Id == Guid.Empty)
            {
                var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                    .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                    .FirstOrDefault();
                if (partner != null)
                {
                    var bank = _mapper.Map<BankAccount>(command.BankAccountRequest);
                    bank.PartnerId = partner.Id;
                    await _unitOfWork.RepositoryFor<BankAccount>().AddAsync(bank);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(bank.Id, "Bank Account Saved Successfully.");
                }
                return await Result<Guid>.FailAsync("Partner Profile Not Verified.");
            }
            else
            {
                var bankAccountInDb = await _unitOfWork.RepositoryFor<BankAccount>().GetByIdAsync(command.BankAccountRequest.Id);
                if (bankAccountInDb != null && bankAccountInDb.DeletedBy == null)
                {
                    bankAccountInDb.BankName = command.BankAccountRequest.BankName ?? bankAccountInDb.BankName;
                    bankAccountInDb.AccountType = command.BankAccountRequest.AccountType ?? bankAccountInDb.AccountType;
                    bankAccountInDb.AccountNo = command.BankAccountRequest.AccountNo ?? bankAccountInDb.AccountNo;
                    bankAccountInDb.ExpiryDate = command.BankAccountRequest.ExpiryDate;
                    bankAccountInDb.CVV = command.BankAccountRequest.CVV;
                    bankAccountInDb.IsActive = command.BankAccountRequest.IsActive;//To Do: Ensure only One Account is Active

                    await _unitOfWork.RepositoryFor<BankAccount>().UpdateAsync(bankAccountInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(bankAccountInDb.Id, "Bank Account Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Bank Account Not Found.");
            }
        }
    }
}
