using AutoMapper;
using CRUD.DAL.Models;
using CRUD.PL.ViewModels;

namespace CRUD.PL.MappingProfile
{
    public class UserProfile:Profile
    {

        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }

    }
}
