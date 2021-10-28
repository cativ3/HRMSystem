using HRMSystem.Core.Entities.Dtos.ApplicationDtos;
using HRMSystem.Core.Entities.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Mappings
{
    public class ApplicantLanguageProfile : Profile
    {
        public ApplicantLanguageProfile()
        {
            CreateMap<ApplicantLanguageAddDto, ApplicantLanguage>()
                .ForMember(
                    x => x.CreatedDate,
                    opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(
                    x => x.IsActive,
                    opt => opt.MapFrom(x => true))
                .ForMember(
                    x => x.IsDeleted,
                    opt => opt.MapFrom(x => false));

            CreateMap<ApplicantLanguage, ApplicantLanguageGetDto>()
                .ForMember(
                    x => x.Language,
                    opt => opt.MapFrom(x => x.Language.Name));
        }
    }
}
