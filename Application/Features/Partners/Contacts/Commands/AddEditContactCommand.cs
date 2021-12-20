using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Partners;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Contacts.Commands
{
    public class AddEditContactCommand : IRequest<Result<Guid>>
    {
        public ContactRequest ContactRequest { get; set; }
    }

    internal class AddEditContactCommandHandler : IRequestHandler<AddEditContactCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        public AddEditContactCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddEditContactCommand command, CancellationToken cancellationToken)
        {
            if (command.ContactRequest.Id == Guid.Empty)
            {
                var address = _mapper.Map<Contact>(command.ContactRequest);
                await _unitOfWork.RepositoryFor<Contact>().AddAsync(address);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(address.Id, "Contact Saved Successfully.");
            }
            else
            {
                var contactInDb = await _unitOfWork.RepositoryFor<Contact>().GetByIdAsync(command.ContactRequest.Id);
                if (contactInDb != null && contactInDb.DeletedBy == null)
                {
                    contactInDb.Title = command.ContactRequest.Title ?? contactInDb.Title;
                    contactInDb.CellphoneNo = command.ContactRequest.CellphoneNo ?? contactInDb.CellphoneNo;
                    contactInDb.TelephoneNo = command.ContactRequest.TelephoneNo ?? contactInDb.TelephoneNo;
                    contactInDb.Email = command.ContactRequest.Email ?? contactInDb.Email;
                    contactInDb.IsActive = command.ContactRequest.IsActive;//To Do: Ensure only One Address is Active

                    await _unitOfWork.RepositoryFor<Contact>().UpdateAsync(contactInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(contactInDb.Id, "Contact Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Contact Not Found.");
            }
        }
    }
}
