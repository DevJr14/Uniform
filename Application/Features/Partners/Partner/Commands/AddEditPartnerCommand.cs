using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Requests.Partners;
using SharedR.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Commands
{
    public class AddEditPartnerCommand : IRequest<Result<Guid>>
    {
        public PartnerRequest PartnerRequest { get; set; }
    }

    internal class AddEditPartnerCommandHandler : IRequestHandler<AddEditPartnerCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public AddEditPartnerCommandHandler(IMapper mapper, IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditPartnerCommand command, CancellationToken cancellationToken)
        {

            if(command.PartnerRequest.Id == Guid.Empty)
            {
                var partner = _mapper.Map<Partner>(command.PartnerRequest);
                partner.UserId = new Guid(_currentUser.UserId);
                await _unitOfWork.RepositoryFor<Partner>().AddAsync(partner);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(partner.Id, "Partner Saved Successfully.");
            }
            else
            {
                var partnerInDb = await _unitOfWork.RepositoryFor<Partner>().GetByIdAsync(command.PartnerRequest.Id);
                if(partnerInDb != null && partnerInDb.DeletedBy == null)
                {
                    partnerInDb.Name = command.PartnerRequest.Name ?? partnerInDb.Name;
                    partnerInDb.Type = command.PartnerRequest.Type ?? partnerInDb.Type;
                    partnerInDb.UserId = command.PartnerRequest.UserId;
                    partnerInDb.RegistrationDate = command.PartnerRequest.RegistrationDate;
                    partnerInDb.RegistrationNo = command.PartnerRequest.RegistrationNo ?? partnerInDb.RegistrationNo;
                    partnerInDb.TaxNo = command.PartnerRequest.TaxNo ?? partnerInDb.TaxNo;
                    partnerInDb.IsVerified = command.PartnerRequest.IsVerified;
                    partnerInDb.Description = command.PartnerRequest.Description ?? partnerInDb.Description;

                    await _unitOfWork.RepositoryFor<Partner>().UpdateAsync(partnerInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(partnerInDb.Id, "Partner Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Partner Not Found.");
            }
        }
    }
}
