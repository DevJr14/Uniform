using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using SharedR.Wrapper;
using System.Threading.Tasks;
using System.Threading;
using System;
using SharedR.Requests.Catalogs;
using Domain.Entities.Catalog;
using Application.Identity.Interfaces;
using Domain.Entities.Partners;
using System.Linq;

namespace Application.Features.Catalogs.Brands.Commands
{
    public class AddEditBrandCommand : IRequest<Result<Guid>>
    {
        public  BrandRequest BrandRequest { get; set; }
    }

    internal class AddEditBrandCommandHandler : IRequestHandler<AddEditBrandCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public AddEditBrandCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid>> Handle(AddEditBrandCommand command, CancellationToken cancellationToken)
        {
            if (command.BrandRequest.Id == Guid.Empty)
            {
                var partner = _unitOfWork.RepositoryFor<Partner>().Entities
                    .Where(p => p.UserId == new Guid(_currentUser.UserId) && p.IsVerified)
                    .FirstOrDefault();
                if (partner != null)
                {
                    var brand = _mapper.Map<Brand>(command.BrandRequest);
                    brand.PartnerId = partner.Id;
                    await _unitOfWork.RepositoryFor<Brand>().AddAsync(brand);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(brand.Id, "Brand Saved Successfully.");
                }
                return await Result<Guid>.FailAsync("Partner Profile Not Verified.");
            }
            else
            {
                var brandInDb = await _unitOfWork.RepositoryFor<Brand>().GetByIdAsync(command.BrandRequest.Id);
                if (brandInDb != null && brandInDb.DeletedBy == null)
                {
                    brandInDb.Name = command.BrandRequest.Name ?? brandInDb.Name;
                    brandInDb.Description = command.BrandRequest.Description ?? brandInDb.Description;

                    await _unitOfWork.RepositoryFor<Brand>().UpdateAsync(brandInDb);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(brandInDb.Id, "Brand Updated Successfully.");
                }
                return await Result<Guid>.FailAsync("Brand Not Found.");
            }
        }
    }
}
