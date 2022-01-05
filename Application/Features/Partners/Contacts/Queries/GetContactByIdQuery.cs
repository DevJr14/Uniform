using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Responses.Partners;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Contacts.Queries
{
    public class GetContactByIdQuery : IRequest<Result<ContactResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Result<ContactResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetContactByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ContactResponse>> Handle(GetContactByIdQuery query, CancellationToken cancellationToken)
        {
            var contactInDb = await _unitOfWork.RepositoryFor<BankAccount>().GetByIdAsync(query.Id);
            if (contactInDb != null && contactInDb.DeletedBy == null)
            {
                var mappedAddress = _mapper.Map<ContactResponse>(contactInDb);
                return await Result<ContactResponse>.SuccessAsync(mappedAddress);
            }
            return await Result<ContactResponse>.FailAsync($"Contact with Id: {query.Id} No Found.");
        }
    }
}
