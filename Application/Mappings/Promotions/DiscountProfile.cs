using AutoMapper;
using Domain.Entities.Promotions;
using SharedR.Requests.Promotions;
using SharedR.Responses.Promotions;

namespace Application.Mappings.Promotions
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<DiscountRequest, Discount>();
            CreateMap<Discount, DiscountResponse>();
        }
    }
}
