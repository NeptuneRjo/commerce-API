using AutoMapper;
using CommerceClone.DTO;
using CommerceClone.Models;

namespace CommerceClone.Profiles
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
