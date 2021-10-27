using HRMSystem.Core.Entities.Dtos.CountryDtos;
using HRMSystem.Core.Utilities.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Abstracts
{
    public interface ICountryService
    {
        Task<IDataResult<IEnumerable<CountryGetDto>>> GetAllWithoutPaging();
        Task<IDataResult<CountryGetDto>> GetById(int countryId);
    }
}
