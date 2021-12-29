using Application.Features.Catalogs.ProductCategories.Commands;
using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductTags.Commands
{
    public class AddEditProductTagsCommand : IRequest<Result<Guid>>
    {
        public ProductTagsRequest ProductTagsRequest { get; set; }
    }

    internal class AddEditProductTagsCommandHandler : IRequestHandler<AddEditProductTagsCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public AddEditProductTagsCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditProductTagsCommand command, CancellationToken cancellationToken)
        {
            //Remove existing product categories before adding new ones.
            //To Do: Don't deleted and add same ProductId and TagIds. Check before deleting
            var proTags = _unitOfWork.RepositoryFor<ProductTag>().Entities
                .Where(pc => pc.ProductId == command.ProductTagsRequest.ProductId)
                .ToList();
            if (proTags.Count > 0)
            {
                foreach (var prodTag in proTags)
                {
                    prodTag.DeletedBy = _currentUser.UserId;
                    prodTag.DeletedOn = DateTime.Now;
                }

                await _unitOfWork.RepositoryFor<ProductTag>().MarkDeletedRangeAsync(proTags);
            }

            if (command.ProductTagsRequest.TagIds.Count() > 0)//Add if any was selected
            {
                List<ProductTag> prodTags = new();
                foreach (Guid tagId in command.ProductTagsRequest.TagIds)
                {
                    prodTags.Add(new ProductTag
                    {
                        ProductId = command.ProductTagsRequest.ProductId,
                        TagId = tagId
                    });
                }
                await _unitOfWork.RepositoryFor<ProductTag>().AddRangeAsync(prodTags);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(command.ProductTagsRequest.ProductId, "Product Tags Updated Successfully.");
            }
            return await Result<Guid>.SuccessAsync(command.ProductTagsRequest.ProductId, "No Product Tags Selected.");
        }
    }
}
