using APIStart.Core.DTOs.EmployeeModelDTOs;
using APIStart.Core.DTOs.ProfessionModelDTOs;
using APIStart.Core.Entities;

using AutoMapper;

namespace APIStart.MappingProfiles
{
    public class MapProfile :Profile
    {
        public MapProfile()
        {
            CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
            CreateMap<EmployeeGetDto, Employee>().ReverseMap();

            CreateMap<ProfessionCreateDto, Profession>().ReverseMap();
            CreateMap<ProfessionUpdateDto, Profession>().ReverseMap();
            CreateMap<ProfessionGetDto, Profession>().ReverseMap();

        }
    }
}
