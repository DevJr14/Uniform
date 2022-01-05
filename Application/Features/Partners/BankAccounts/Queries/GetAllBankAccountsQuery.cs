using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.BankAccounts.Queries
{
    public class GetAllBankAccountsQuery : IRequest<Result<List<BankAccountResponse>>>
    {
    }

    internal class GetAllBankAccountsQueryHandler : IRequestHandler<GetAllBankAccountsQuery, Result<List<BankAccountResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllBankAccountsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<BankAccountResponse>>> Handle(GetAllBankAccountsQuery query, CancellationToken cancellationToken)
        {
            var bankAccounts = await _unitOfWork.RepositoryFor<BankAccount>().GetAllAsync();
            if (bankAccounts.Where(a => a.DeletedBy == null).Count() > 0)
            {
                var mappedBankAccounts = _mapper.Map<List<BankAccountResponse>>(bankAccounts.Where(a => a.DeletedBy == null));
                return await Result<List<BankAccountResponse>>.SuccessAsync(mappedBankAccounts);
            }
            return await Result<List<BankAccountResponse>>.FailAsync("No Records Found.");
        }
    }
}
