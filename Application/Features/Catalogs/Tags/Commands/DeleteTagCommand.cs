using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using SharedR.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Tags.Commands
{
    public class DeleteTagCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteTagCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(DeleteTagCommand command, CancellationToken cancellationToken)
        {
            var TagInDb = await _unitOfWork.RepositoryFor<Tag>().GetByIdAsync(command.Id);
            if (TagInDb != null && TagInDb.DeletedBy == null)
            {
                TagInDb.DeletedBy = _currentUser.UserId;
                TagInDb.DeletedOn = DateTime.Now;

                await _unitOfWork.RepositoryFor<Tag>().MarkDeletedAsync(TagInDb);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(TagInDb.Id, "Tag Deleted Successfully.");
            }
            return await Result<Guid>.FailAsync($"Tag with Id: {command.Id} Not Found.");
        }
    }
}
