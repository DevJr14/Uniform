using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Collections.Generic;
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

        public AddEditProductCategoriesCommandHandler(IUnitOfWork<Guid> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddEditProductCategoriesCommand command, CancellationToken cancellationToken)
        {
            if(command.ProductCategoriesRequest.CategoryIds.Count > 0)//Add if any was selected
            {
                List<ProductCategory> productCategories = new();
                foreach (Guid prodCategoryId in command.ProductCategoriesRequest.CategoryIds)
                {
                    productCategories.Add(new ProductCategory
                    {
                        ProductId = command.ProductCategoriesRequest.ProductId,
                        CategoryId = prodCategoryId
                    });
                }
                await _unitOfWork.RepositoryFor<ProductCategory>().AddRangeAsync(productCategories);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(command.ProductCategoriesRequest.ProductId, "Product Categories Saved Successfully.");
            }
            return await Result<Guid>.SuccessAsync(command.ProductCategoriesRequest.ProductId, "No Product Categories Selected.");
        }
    }
}
