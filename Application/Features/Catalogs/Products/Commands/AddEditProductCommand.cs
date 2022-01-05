using Application.Identity.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Catalog;
using Domain.Entities.Partners;
using MediatR;
using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.Products.Commands
{
    public class AddEditProductCommand : IRequest<Result<Guid>>
    {
        public ProductRequest ProductRequest { get; set; }
    }

    internal class AddEditProductCommandHandler : IRequestHandler<AddEditProductCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public AddEditProductCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditProductCommand command, CancellationToken cancellationToken)
        {
            if (command.ProductRequest.Id == Guid.Empty)
            {
                var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                    .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                    .FirstOrDefault();
                if (partner != null)
                {
                    var product = _mapper.Map<Product>(command.ProductRequest);
                    product.PartnerId = partner.Id;
                    await _unitOfWork.RepositoryFor<Product>().AddAsync(product);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(product.Id, "Product Saved Successfully.");
                }
                return await Result<Guid>.FailAsync("Partner Profile Not Verified.");
            }
            else
            {
                var productInDb = await _unitOfWork.RepositoryFor<Product>().GetByIdAsync(command.ProductRequest.Id);
                if (productInDb != null && productInDb.DeletedBy == null)
                {
                    productInDb.Name = command.ProductRequest.Name ?? productInDb.Name;
                    productInDb.Description = command.ProductRequest.Description ?? productInDb.Description;
                    productInDb.Barcode = command.ProductRequest.Barcode ?? productInDb.Barcode;
                    productInDb.IsActive = command.ProductRequest.IsActive;
                    productInDb.AllowReview = command.ProductRequest.AllowReview;
                    productInDb.AvailableFrom = command.ProductRequest.AvailableFrom;
                    productInDb.AvailableTo = command.ProductRequest.AvailableTo;
                    productInDb.IsNew = command.ProductRequest.IsNew;
                    productInDb.BrandId = command.ProductRequest.BrandId;

                    await _unitOfWork.RepositoryFor<Product>().UpdateAsync(productInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(productInDb.Id, "Product Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Product Not Found.");
            }
        }
    }
}
