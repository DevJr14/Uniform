using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductCategories.Commands
{
    public class AddEditProductCategoriesCommand : IRequest<Result<Guid>>
    {
        public ProductCategoriesRequest ProductCategoriesRequest { get; set; }
    }

    internal class AddEditProductCategoriesCommandHandler : IRequestHandler<AddEditProductCategoriesCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public AddEditProductCategoriesCommandHandler(IUnitOfWork<Guid> unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditProductCategoriesCommand command, CancellationToken cancellationToken)
        {
            //Remove existing product categories before adding new ones.
            //To Do: Don't deleted and add same ProductId and CategoryIds. Check before deleting
            var proCategories = _unitOfWork.RepositoryFor<ProductCategory>().Entities
                .Where(pc => pc.ProductId == command.ProductCategoriesRequest.ProductId)
                .ToList();
            if (proCategories.Count > 0)
            {
                foreach (var prodCat in proCategories)
                {
                    prodCat.DeletedBy = _currentUser.UserId;
                    prodCat.DeletedOn = DateTime.Now;
                }

                await _unitOfWork.RepositoryFor<ProductCategory>().MarkDeletedRangeAsync(proCategories);
            }

            if(command.ProductCategoriesRequest.CategoryIds.Count > 0)//Add if any was selected
            {
                List<ProductCategory> productCategories = new();
                foreach (Guid categoryId in command.ProductCategoriesRequest.CategoryIds)
                {
                    productCategories.Add(new ProductCategory
                    {
                        ProductId = command.ProductCategoriesRequest.ProductId,
                        CategoryId = categoryId
                    });
                }
                await _unitOfWork.RepositoryFor<ProductCategory>().AddRangeAsync(productCategories);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(command.ProductCategoriesRequest.ProductId, "Product Categories Updated Successfully.");
            }
            return await Result<Guid>.SuccessAsync(command.ProductCategoriesRequest.ProductId, "No Product Categories Selected.");
        }
    }
}
