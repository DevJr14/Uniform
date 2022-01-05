using Application.Features.Catalogs.ProductPrices.Commands;
using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Tags.Commands
{
    public class AddEditTagCommand : IRequest<Result<Guid>>
    {
        public TagRequest TagRequest { get; set; }
    }

    internal class AddEditTagCommandHandler : IRequestHandler<AddEditTagCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public AddEditTagCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditTagCommand command, CancellationToken cancellationToken)
        {
            if (command.TagRequest.Id == Guid.Empty)
            {
                var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                    .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                    .FirstOrDefault();
                if (partner != null)
                {
                    var tag = _mapper.Map<Tag>(command.TagRequest);
                    tag.PartnerId = partner.Id;
                    await _unitOfWork.RepositoryFor<Tag>().AddAsync(tag);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(tag.Id, "Tag Saved Successfully.");
                }
                return await Result<Guid>.FailAsync("Partner Profile Not Verified.");
            }
            else
            {
                var tagInDb = await _unitOfWork.RepositoryFor<Tag>().GetByIdAsync(command.TagRequest.Id);
                if (tagInDb != null && tagInDb.DeletedBy == null)
                {
                    tagInDb.Name = command.TagRequest.Name ?? tagInDb.Name;

                    await _unitOfWork.RepositoryFor<Tag>().UpdateAsync(tagInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(tagInDb.Id, "Tag Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Tag Not Found.");
            }
        }
    }
}
