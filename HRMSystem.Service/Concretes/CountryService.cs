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
    public class CountryService : ICountryService
    {
        private readonly HRManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public CountryService(HRManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IDataResult<IEnumerable<CountryGetDto>>> GetAllWithoutPaging()
        {
            var countriesQuery = _dbContext.Countries.AsNoTracking();

            var countryDtos = await countriesQuery.Select(country => _mapper.Map<CountryGetDto>(country)).ToListAsync();

            return new DataResult<IEnumerable<CountryGetDto>>(ResultStatus.Success, countryDtos);
        }

        public async Task<IDataResult<CountryGetDto>> GetById(int countryId)
        {
            var country = await _dbContext.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == countryId);

            if (country is null) throw new ArgumentNotFoundException(new Error("countryId", "Country not found."));

            var countryDto = _mapper.Map<CountryGetDto>(country);

            return new DataResult<CountryGetDto>(ResultStatus.Success, countryDto);
        }
    }
}
