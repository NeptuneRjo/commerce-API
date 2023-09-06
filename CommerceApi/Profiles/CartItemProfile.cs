using AutoMapper;
using CommerceApi.DTO;
using CommerceClone.Models;

namespace CommerceApi.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<UpdateCartModel, CartItem>();
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
