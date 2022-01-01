using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalogs.ProductImages.Commands
{
    public class AddEditProductImageCommand : IRequest<Result<Guid>>
    {
        public ProductImageRequest ProductImageRequest { get; set; }
    }

    internal class AddEditProductImageCommandHandler : IRequestHandler<AddEditProductImageCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        public AddEditProductImageCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<Result<Guid>> Handle(AddEditProductImageCommand command, CancellationToken cancellationToken)
        {
            var uploadRequest = command.ProductImageRequest.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"{command.ProductImageRequest.Title}-{command.ProductImageRequest.ProductId}-{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{command.ProductImageRequest.UploadRequest.Extension}";
            }

            if (command.ProductImageRequest.Id == Guid.Empty)
            {
                var productImage = _mapper.Map<ProductImage>(command.ProductImageRequest);
                if (uploadRequest != null)
                {
                    productImage.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                }
                await _unitOfWork.RepositoryFor<ProductImage>().AddAsync(productImage);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(productImage.Id, "Product Image Saved Successfully.");
            }
            else
            {
                var productImageInDb = await _unitOfWork.RepositoryFor<ProductImage>().GetByIdAsync(command.ProductImageRequest.Id);
                if (productImageInDb != null && productImageInDb.DeletedBy == null)
                {
                    if (uploadRequest != null)
                    {
                        productImageInDb.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                    }
                    productImageInDb.Title = command.ProductImageRequest.Title;
                    productImageInDb.AltText = command.ProductImageRequest.AltText;

                    await _unitOfWork.RepositoryFor<ProductImage>().UpdateAsync(productImageInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(productImageInDb.Id, "Product Image Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Product Image Not Found.");
            }
        }
    }
}
