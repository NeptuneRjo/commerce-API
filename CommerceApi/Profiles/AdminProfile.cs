using AutoMapper;
using CommerceApi.DTO;
using CommerceClone.Models;

namespace CommerceApi.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminModel, Admin>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        }
    }
}
