using AutoMapper;
using Domain.Entities.Catalog;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;

namespace Application.Mappings.Catalogs
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandRequest, Brand>();
            CreateMap<Brand, BrandResponse>();
        }
    }
}
