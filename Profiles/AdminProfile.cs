using AutoMapper;
using CommerceClone.DTO;
using CommerceClone.Models;

namespace CommerceClone.Profiles
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
