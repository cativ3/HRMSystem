using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.Core.Entities.Dtos.LanguageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Abstracts
{
    public interface ILanguageService
    {
        Task<IDataResult<IEnumerable<LanguageGetDto>>> GetAllWithoutPaging();
        Task<IDataResult<LanguageGetDto>> GetById(int languageId);
    }
}
