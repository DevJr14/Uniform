using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.BankAccounts.Queries
{
    public class GetAllBankAccountsForPartnerQuery : IRequest<Result<List<BankAccountResponse>>>
    {
        public Guid PartnerId { get; set; }
    }

    internal class GetAllBankAccountsForPartnerQueryHandler : IRequestHandler<GetAllBankAccountsForPartnerQuery, Result<List<BankAccountResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllBankAccountsForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<BankAccountResponse>>> Handle(GetAllBankAccountsForPartnerQuery query, CancellationToken cancellationToken)
        {
            var bankAccounts = _unitOfWork.RepositoryFor<Address>().Entities
                .Where(ba => ba.PartnerId == query.PartnerId && ba.DeletedBy == null)
                .ToList();
            if (bankAccounts.Count > 0)
            {
                var mappedBankAccounts = _mapper.Map<List<BankAccountResponse>>(bankAccounts);
                return await Result<List<BankAccountResponse>>.SuccessAsync(mappedBankAccounts);
            }
            return await Result<List<BankAccountResponse>>.FailAsync("No Records Found.");
        }
    }
}
