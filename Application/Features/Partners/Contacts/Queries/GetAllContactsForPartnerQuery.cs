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

namespace Application.Features.Partners.Contacts.Queries
{
    public class GetAllContactsForPartnerQuery : IRequest<Result<List<ContactResponse>>>
    {
        public Guid PartnerId { get; set; }
    }

    internal class GetAllContactsForPartnerQueryHandler : IRequestHandler<GetAllContactsForPartnerQuery, Result<List<ContactResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllContactsForPartnerQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ContactResponse>>> Handle(GetAllContactsForPartnerQuery query, CancellationToken cancellationToken)
        {
            var contacts = _unitOfWork.RepositoryFor<Contact>().Entities
                .Where(a => a.PartnerId == query.PartnerId && a.DeletedBy == null)
                .ToList();
            if (contacts.Count > 0)
            {
                var mappedContacts = _mapper.Map<List<ContactResponse>>(contacts);
                return await Result<List<ContactResponse>>.SuccessAsync(mappedContacts);
            }
            return await Result<List<ContactResponse>>.FailAsync("No Records Found.");
        }
    }
}
