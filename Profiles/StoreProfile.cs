using AutoMapper;
using CommerceClone.DTO;
using CommerceClone.Models;

namespace CommerceClone.Profiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Store, StoreDto>();
        }
    }
}
