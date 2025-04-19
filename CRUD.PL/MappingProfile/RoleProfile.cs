using AutoMapper;
using CRUD.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CRUD.PL.MappingProfile
{
    public class RoleProfile:Profile
    {

        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ForMember(o=>o.RoleName,o=>o.MapFrom(s=>s.Name)).ReverseMap();
        }
    }
}
