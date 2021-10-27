using AutoMapper;
using HRMSystem.Core.Entities.Concrete;
using HRMSystem.Core.Entities.Dtos.CityDtos;
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
    public class CityService : ServiceBase, ICityService
    {
        public CityService(HRManagementDbContext dbContext, IMapper mapper):base(dbContext, mapper)
        {

        }

        public async Task<IDataResult<IEnumerable<CityGetDto>>> GetAllWithoutPaging(int countryId)
        {
            var citiesQuery = DbContext.Cities.AsNoTracking().Where(x => x.CountryId == countryId);

            var citiesListDto = await citiesQuery.Select(city => Mapper.Map<CityGetDto>(city)).ToListAsync();

            return new DataResult<IEnumerable<CityGetDto>>(ResultStatus.Success, citiesListDto);
        }

        public async Task<IDataResult<CityGetDto>> GetById(int cityId)
        {
            var city = await DbContext.Cities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == cityId);

            if (city is null) throw new ArgumentNotFoundException(new Error("CityId", "City was not found."));

            var cityGetDto = Mapper.Map<CityGetDto>(city);

            return new DataResult<CityGetDto>(ResultStatus.Success, cityGetDto);
        }
    }
}
