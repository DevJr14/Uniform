using AutoMapper;
using Domain.Entities.Catalog;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;

namespace Application.Mappings.Catalogs
{
    public class ProductTagsProfile : Profile
    {
        public ProductTagsProfile()
        {
            CreateMap<ProductTagsRequest, ProductTags>();
            CreateMap<ProductTags, ProductTagsResponse>();
        }
    }
}
