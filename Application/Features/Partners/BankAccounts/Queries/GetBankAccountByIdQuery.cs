using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Responses.Partners;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.BankAccounts.Queries
{
    public class GetBankAccountByIdQuery : IRequest<Result<BankAccountResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetBankAccountByIdQueryHandler : IRequestHandler<GetBankAccountByIdQuery, Result<BankAccountResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetBankAccountByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<BankAccountResponse>> Handle(GetBankAccountByIdQuery query, CancellationToken cancellationToken)
        {
            var bankAccountInDb = await _unitOfWork.RepositoryFor<BankAccount>().GetByIdAsync(query.Id);
            if (bankAccountInDb != null && bankAccountInDb.DeletedBy == null)
            {
                var mappedAddress = _mapper.Map<BankAccountResponse>(bankAccountInDb);
                return await Result<BankAccountResponse>.SuccessAsync(mappedAddress);
            }
            return await Result<BankAccountResponse>.FailAsync($"Bank Account with Id: {query.Id} No Found.");
        }
    }
}
