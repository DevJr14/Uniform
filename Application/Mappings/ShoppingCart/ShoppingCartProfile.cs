using AutoMapper;
using Domain.Entities.ShoppingCarts;
using SharedR.Requests.ShoppingCart;
using SharedR.Responses.ShoppingCart;

namespace Application.Mappings.ShoppingCarts 
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<CartHeaderRequest, CartHeader>();
            CreateMap<CartHeader, CartHeaderResponse>();

            CreateMap<CartDetailsRequest, CartDetails>();
            CreateMap<CartDetails, CartDetailsResponse>();

            CreateMap<ShoppingCartRequest, ShoppingCart>();
            CreateMap<ShoppingCart, ShoppingCartResponse>();
        }
    }
}
