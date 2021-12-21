using AutoMapper;
using Domain.Entities.Catalog;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;

namespace Application.Mappings.Catalogs
{
    public class ProductReviewsProfile : Profile
    {
        public ProductReviewsProfile()
        {
            CreateMap<ProductReviewRequest, ProductReview>();
            CreateMap<ProductReview, ProductReviewResponse>();
        }
    }
}
