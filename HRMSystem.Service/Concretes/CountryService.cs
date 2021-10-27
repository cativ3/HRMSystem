using AutoMapper;
using HRMSystem.Core.Entities.Dtos.CountryDtos;
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
    public class CountryService : ServiceBase, ICountryService
    {
        public CountryService(HRManagementDbContext dbContext, IMapper mapper):base(dbContext, mapper)
        {

        }
        public async Task<IDataResult<IEnumerable<CountryGetDto>>> GetAllWithoutPaging()
        {
            var countriesQuery = DbContext.Countries.AsNoTracking();

            var countryDtos = await countriesQuery.Select(country => Mapper.Map<CountryGetDto>(country)).ToListAsync();

            return new DataResult<IEnumerable<CountryGetDto>>(ResultStatus.Success, countryDtos);
        }

        public async Task<IDataResult<CountryGetDto>> GetById(int countryId)
        {
            var country = await DbContext.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == countryId);

            if (country is null) throw new ArgumentNotFoundException(new Error("countryId", "Country not found."));

            var countryDto = Mapper.Map<CountryGetDto>(country);

            return new DataResult<CountryGetDto>(ResultStatus.Success, countryDto);
        }
    }
}
