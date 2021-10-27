using AutoMapper;
using HRMSystem.Core.Entities.Dtos.LanguageDtos;
using HRMSystem.Core.Utilities.Exceptions;
using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.Core.Utilities.Results.ComplexTypes;
using HRMSystem.Core.Utilities.Results.Concretes;
using HRMSystem.DataAccess.Contexts;
using HRMSystem.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Concretes
{
    public class LanguageService : ServiceBase, ILanguageService
    {
        public LanguageService(HRManagementDbContext dbContext, IMapper mapper):base(dbContext, mapper)
        {
            
        }

        public async Task<IDataResult<IEnumerable<LanguageGetDto>>> GetAllWithoutPaging()
        {
            var languagesQuery = DbContext.Languages.AsNoTracking();

            var languageDtos = await languagesQuery.Select(language => Mapper.Map<LanguageGetDto>(language)).ToListAsync();

            return new DataResult<IEnumerable<LanguageGetDto>>(ResultStatus.Success, languageDtos);
        }

        public async Task<IDataResult<LanguageGetDto>> GetById(int languageId)
        {
            var language = await DbContext.Languages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == languageId);

            if (language is null) throw new ArgumentNotFoundException(new Error("languageId", "Language not found"));

            var languageDto = Mapper.Map<LanguageGetDto>(language);

            return new DataResult<LanguageGetDto>(ResultStatus.Success, languageDto);
        }
    }
}
