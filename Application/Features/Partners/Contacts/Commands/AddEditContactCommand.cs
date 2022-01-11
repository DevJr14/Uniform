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
        private readonly ICurrentUserService _currentUser;
        public AddEditContactCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditContactCommand command, CancellationToken cancellationToken)
        {
            if (command.ContactRequest.Id == Guid.Empty)
            {
                var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                    .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.Id == command.ContactRequest.PartnerId)
                    .FirstOrDefault();
                if (partner != null)
                {
                    var contact = _mapper.Map<Contact>(command.ContactRequest);
                    contact.PartnerId = partner.Id;
                    await _unitOfWork.RepositoryFor<Contact>().AddAsync(contact);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(contact.Id, "Contact Saved Successfully.");
                }
                return await Result<Guid>.FailAsync("No Partner Profile Found.");
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
