using AutoMapper;
using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;

namespace CommerceApi.BLL.Utilities.AutoMapperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ItemDto>();
            CreateMap<Cart, CartDto>();
        }

    }
}
