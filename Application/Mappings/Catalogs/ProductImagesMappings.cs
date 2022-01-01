using AutoMapper;
using Domain.Entities.Catalog;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;

namespace Application.Mappings.Catalogs
{
    public class ProductImagesMappings : Profile
    {
        public ProductImagesMappings()
        {
            CreateMap<ProductImageRequest, ProductImage>();
            CreateMap<ProductImage, ProductImageResponse>();
        }
    }
}
