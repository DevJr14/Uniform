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

namespace Application.Features.Partners.Contacts.Queries
{
    public class GetAllContactsQuery : IRequest<Result<List<ContactResponse>>>
    {
    }

    internal class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, Result<List<ContactResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllContactsQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ContactResponse>>> Handle(GetAllContactsQuery query, CancellationToken cancellationToken)
        {
            var contacts = await _unitOfWork.RepositoryFor<Contact>().GetAllAsync();
            if (contacts.Where(a => a.DeletedBy == null).Count() > 0)
            {
                var mappedContacts = _mapper.Map<List<ContactResponse>>(contacts.Where(a => a.DeletedBy == null));
                return await Result<List<ContactResponse>>.SuccessAsync(mappedContacts);
            }
            return await Result<List<ContactResponse>>.FailAsync("No Records Found.");
        }
    }
}
