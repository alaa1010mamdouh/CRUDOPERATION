using AutoMapper;
using CRUD.DAL.Models;
using CRUD.PL.ViewModels;

namespace CRUD.PL.MappingProfile
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap()/*.ForMember(d=>d.Name,o=>o.MapFrom(s=>s.Name))*/;   
        }
    }
}
