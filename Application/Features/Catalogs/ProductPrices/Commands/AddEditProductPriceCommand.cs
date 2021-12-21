using Application.Features.Catalogs.Brands.Commands;
using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using Domain.Entities.Partners;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductPrices.Commands
{
    public class AddEditProductPriceCommand : IRequest<Result<Guid>>
    {
        public ProductPriceRequest ProductPriceRequest { get; set; }
    }

    internal class AddEditProductPriceCommandHandler : IRequestHandler<AddEditProductPriceCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        public AddEditProductPriceCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddEditProductPriceCommand command, CancellationToken cancellationToken)
        {
            if (command.ProductPriceRequest.Id == Guid.Empty)
            {  
                var productPrice = _mapper.Map<ProductPrice>(command.ProductPriceRequest);
                await _unitOfWork.RepositoryFor<ProductPrice>().AddAsync(productPrice);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(productPrice.Id, "Product Price Saved Successfully.");                
            }
            else
            {
                var productPriceInDb = await _unitOfWork.RepositoryFor<ProductPrice>().GetByIdAsync(command.ProductPriceRequest.Id);
                if (productPriceInDb != null && productPriceInDb.DeletedBy == null)
                {
                    productPriceInDb.Price = command.ProductPriceRequest.Price;
                    productPriceInDb.OldPrice = command.ProductPriceRequest.OldPrice;
                    productPriceInDb.Cost = command.ProductPriceRequest.Cost;
                    productPriceInDb.DiscountId = command.ProductPriceRequest.DiscountId;

                    await _unitOfWork.RepositoryFor<ProductPrice>().UpdateAsync(productPriceInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(productPriceInDb.Id, "Product Price Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Product Price Not Found.");
            }
        }
    }
}
