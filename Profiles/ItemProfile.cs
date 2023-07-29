using AutoMapper;
using CommerceClone.DTO;
using CommerceClone.Models;

namespace CommerceClone.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<ItemModel, Item>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
