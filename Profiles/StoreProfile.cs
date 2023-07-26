using AutoMapper;
using CommerceClone.Controllers;
using CommerceClone.DTO;
using CommerceClone.Models;

namespace CommerceClone.Profiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Store, StoreDto>();
            CreateMap<StoreModel, Store>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
