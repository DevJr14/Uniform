using AutoMapper;
using Domain.Entities.Catalog;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;

namespace Application.Mappings.Catalogs
{
    public class ProductCategoriesProfile : Profile
    {
        public ProductCategoriesProfile()
        {
            CreateMap<ProductCategoriesRequest, ProductCategories>();
            CreateMap<ProductCategories, ProductCategoriesResponse>();
        }
    }
}
