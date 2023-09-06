using AutoMapper;
using CommerceApi.DTO;
using CommerceClone.Models;

namespace CommerceApi.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Product, ItemDto>();
            CreateMap<ItemModel, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
