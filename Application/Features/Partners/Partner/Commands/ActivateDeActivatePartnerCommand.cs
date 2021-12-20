using Application.Interfaces.Repositories;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Partners.Commands
{
    public class ActivateDeActivatePartnerCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class ActivateDeActivatePartnerCommandHandler : IRequestHandler<ActivateDeActivatePartnerCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public ActivateDeActivatePartnerCommandHandler(IUnitOfWork<Guid> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ActivateDeActivatePartnerCommand command, CancellationToken cancellationToken)
        {
            var partner = await _unitOfWork.RepositoryFor<Partner>().GetByIdAsync(command.Id);
            if (partner != null)
            {
                partner.IsVerified = !partner.IsVerified;
                await _unitOfWork.RepositoryFor<Partner>().UpdateAsync(partner);
                await _unitOfWork.Commit(cancellationToken);

                return await Result<Guid>.SuccessAsync(partner.Id, partner.IsVerified? "Partner Actiated Successfully." : "Partner De-Actiated Successfully.");
            }
            return await Result<Guid>.FailAsync($"Partner with Id: {command.Id} Not Found. Failed.");
        }
    }
}
