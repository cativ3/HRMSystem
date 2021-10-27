using AutoMapper;
using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Concrete;
using HRMSystem.Core.Entities.Dtos.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeAddDto, Employee>()
                .ForMember(
                    x => x.WorkingStatus,
                    opt => opt.MapFrom(x => WorkingStatus.Working))
                .ForMember(
                    x => x.StartingDate,
                    opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(
                    x => x.IsActive,
                    opt => opt.MapFrom(x => true))
                .ForMember(
                    x => x.IsDeleted,
                    opt => opt.MapFrom(x => false));
        }
    }
}
