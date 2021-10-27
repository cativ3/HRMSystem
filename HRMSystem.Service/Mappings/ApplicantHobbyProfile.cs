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
    public class ApplicantHobbyProfile : Profile
    {
        public ApplicantHobbyProfile()
        {
            CreateMap<ApplicantHobbyAddDto, ApplicantHobby>()
                .ForMember(
                    x => x.CreatedDate,
                    opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(
                    x => x.IsActive,
                    opt => opt.MapFrom(x => true))
                .ForMember(
                    x => x.IsDeleted,
                    opt => opt.MapFrom(x => false));

            CreateMap<ApplicantHobby, ApplicantHobbyGetDto>();
        }
    }
}
