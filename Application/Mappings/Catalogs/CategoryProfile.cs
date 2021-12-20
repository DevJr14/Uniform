using AutoMapper;
using Domain.Entities.Catalog;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;

namespace Application.Mappings.Catalogs
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>();
        }
    }
}
