using AutoMapper;
using CommerceApi.DTO;
using CommerceClone.Models;

namespace CommerceApi.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>();
        }
    }
}
