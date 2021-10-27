using AutoMapper;
using HRMSystem.Core.Entities.Concrete;
using HRMSystem.Core.Entities.Dtos.ApplicationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationAddDto, Application>()
                .ForMember(
                    x => x.CreatedDate,
                    opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(
                    x => x.IsActive,
                    opt => opt.MapFrom(x => true))
                .ForMember(
                    x => x.IsDeleted,
                    opt => opt.MapFrom(x => false));

            CreateMap<Application, ApplicationGetDto>()
                .ForMember(
                    x => x.WorkTitle,
                    opt => opt.MapFrom(x => x.WorkTitle.Name))
                .ForMember(
                    x => x.City,
                    opt => opt.MapFrom(x => x.City.Name))
                .ForMember(
                    x => x.Country,
                    opt => opt.MapFrom(x => x.Country.Name));
        }
    }
}
