using HRMSystem.Core.Entities.Dtos.CityDtos;
using HRMSystem.Core.Utilities.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Abstracts
{
    public interface ICityService
    {
        Task<IDataResult<IEnumerable<CityGetDto>>> GetAllWithoutPaging(int countryId);
        Task<IDataResult<CityGetDto>> GetById(int cityId);
    }
}
