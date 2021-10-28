using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Concrete;
using HRMSystem.Core.Entities.Dtos.ApplicationDtos;
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
using System.Transactions;

namespace HRMSystem.Service.Concretes
{
    public class ApplicationService : ServiceBase, IApplicationService
    {
        private readonly IValidator<Application> _applicationValidator;
        private readonly IValidator<ApplicantEducation> _applicantEducationValidator;
        private readonly IValidator<ApplicantHobby> _applicantHobbyValidator;
        private readonly IValidator<ApplicantLanguage> _applicantLanguageValidator;
        private readonly IValidator<ApplicantWorkExperience> _applicantWorkExperienceValidator;

        public ApplicationService(
            HRManagementDbContext dbContext,
            IMapper mapper,
            IValidator<Application> applicationValidator,
            IValidator<ApplicantEducation> applicantEducationValidator, 
            IValidator<ApplicantHobby> applicantHobbyValidator, 
            IValidator<ApplicantLanguage> applicantLanguageValidator, 
            IValidator<ApplicantWorkExperience> applicantWorkExperienceValidator) : base(dbContext, mapper)
        {
            _applicationValidator = applicationValidator;
            _applicantEducationValidator = applicantEducationValidator;
            _applicantHobbyValidator = applicantHobbyValidator;
            _applicantLanguageValidator = applicantLanguageValidator;
            _applicantWorkExperienceValidator = applicantWorkExperienceValidator;
        }

        public async Task<IDataResult<ApplicationListDto>> GetAllAsync(int? workTitleId, int currentPage, int pageSize, bool? isActive, bool? isDeleted, ApplicationOrderBy orderBy, bool isAscending, string searchKeyword)
        {
            IQueryable<Application> query = DbContext.Set<Application>().AsQueryable();


            // Filters

            if (workTitleId.HasValue) query = query.Where(x => x.WorkTitleId == workTitleId);
            if (isActive.HasValue) query = query.Where(x => x.IsActive == isActive);
            if (isDeleted.HasValue) query = query.Where(x => x.IsDeleted == isDeleted);
            if (!string.IsNullOrEmpty(searchKeyword)) query = query.Where(x => x.FirstName.Contains(searchKeyword) || x.LastName.Contains(searchKeyword));


            // Includes

            query = query.Include(x => x.WorkTitle);
            query = query.Include(x => x.Country);
            query = query.Include(x => x.City);
            query = query.Include(x => x.ApplicantEducations);
            query = query.Include(x => x.ApplicantHobbies);
            query = query.Include(x => x.ApplicantLanguages).ThenInclude(x => x.Language);
            query = query.Include(x => x.ApplicantWorkExperiences);


            pageSize = pageSize > 100 ? 100 : pageSize;

            var totalCount = await query.CountAsync();

            switch (orderBy)
            {
                case ApplicationOrderBy.Fullname:
                    query = isAscending
                        ? query.OrderBy(x => x.FirstName)
                        : query.OrderByDescending(x => x.FirstName);
                    break;
                case ApplicationOrderBy.Email:
                    query = isAscending
                        ? query.OrderBy(x => x.Email)
                        : query.OrderByDescending(x => x.Email);
                    break;
                case ApplicationOrderBy.PhoneNumber:
                    query = isAscending
                        ? query.OrderBy(x => x.PhoneNumber)
                        : query.OrderByDescending(x => x.PhoneNumber);
                    break;
                case ApplicationOrderBy.WorkTitle:
                    query = isAscending
                        ? query.OrderBy(x => x.WorkTitle.Name)
                        : query.OrderByDescending(x => x.WorkTitle.Name);
                    break;
                default:
                    query = isAscending
                        ? query.OrderBy(x => x.AppliedDate)
                        : query.OrderByDescending(x => x.AppliedDate);
                    break;
            }

            var applicationListDto = new ApplicationListDto
            {
                Applications = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => Mapper.Map<ApplicationGetDto>(x))
                    .ToListAsync(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount,
                IsAscending = isAscending
            };

            return new DataResult<ApplicationListDto>(ResultStatus.Success, applicationListDto);
        }

        public async Task<IDataResult<IEnumerable<ApplicationGetDto>>> GetAllWithoutPagingAsync(int? workTitleId, bool? isActive, bool? isDeleted, ApplicationOrderBy orderBy, bool isAscending, string searchKeyword)
        {
            IQueryable<Application> query = DbContext.Set<Application>().AsQueryable();


            // Filters

            if (workTitleId.HasValue) query = query.Where(x => x.WorkTitleId == workTitleId);
            if (isActive.HasValue) query = query.Where(x => x.IsActive == isActive);
            if (isDeleted.HasValue) query = query.Where(x => x.IsDeleted == isDeleted);
            if (!string.IsNullOrEmpty(searchKeyword)) query = query.Where(x => x.FirstName.Contains(searchKeyword) || x.LastName.Contains(searchKeyword));


            // Includes

            query = query.Include(x => x.WorkTitle);
            query = query.Include(x => x.Country);
            query = query.Include(x => x.City);
            query = query.Include(x => x.ApplicantEducations);
            query = query.Include(x => x.ApplicantHobbies);
            query = query.Include(x => x.ApplicantLanguages).ThenInclude(x => x.Language);
            query = query.Include(x => x.ApplicantWorkExperiences);

            switch (orderBy)
            {
                case ApplicationOrderBy.Fullname:
                    query = isAscending
                        ? query.OrderBy(x => x.FirstName)
                        : query.OrderByDescending(x => x.FirstName);
                    break;
                case ApplicationOrderBy.Email:
                    query = isAscending
                        ? query.OrderBy(x => x.Email)
                        : query.OrderByDescending(x => x.Email);
                    break;
                case ApplicationOrderBy.PhoneNumber:
                    query = isAscending
                        ? query.OrderBy(x => x.PhoneNumber)
                        : query.OrderByDescending(x => x.PhoneNumber);
                    break;
                case ApplicationOrderBy.WorkTitle:
                    query = isAscending
                        ? query.OrderBy(x => x.WorkTitle.Name)
                        : query.OrderByDescending(x => x.WorkTitle.Name);
                    break;
                default:
                    query = isAscending
                        ? query.OrderBy(x => x.AppliedDate)
                        : query.OrderByDescending(x => x.AppliedDate);
                    break;
            }

            var applicationGetDtos = await query.Select(x => Mapper.Map<ApplicationGetDto>(x)).ToListAsync();

            return new DataResult<IEnumerable<ApplicationGetDto>>(ResultStatus.Success, applicationGetDtos);
        }

        public async Task<IDataResult<ApplicationGetDto>> GetByIdAsync(Guid applicationId)
        {
            IQueryable<Application> query = DbContext.Set<Application>().AsQueryable();

            // Includes

            query = query.Include(x => x.WorkTitle);
            query = query.Include(x => x.Country);
            query = query.Include(x => x.City);
            query = query.Include(x => x.ApplicantEducations);
            query = query.Include(x => x.ApplicantHobbies);
            query = query.Include(x => x.ApplicantLanguages).ThenInclude(x => x.Language);
            query = query.Include(x => x.ApplicantWorkExperiences);

            var application = await query.FirstOrDefaultAsync(x => x.Id == applicationId);

            if (application is null) throw new ArgumentNotFoundException(new Error("applicationId", "Application was not found."));

            var applicationGetDto = Mapper.Map<ApplicationGetDto>(application);

            return new DataResult<ApplicationGetDto>(ResultStatus.Success, applicationGetDto);
        }

        public async Task<IDataResult<ApplicationGetDto>> AddAsync(ApplicationAddDto applicationAddDto)
        {
            var application = Mapper.Map<Application>(applicationAddDto);

            var applicationContext = new ValidationContext<Application>(application);
            var applicationValidationResult = await _applicationValidator.ValidateAsync(applicationContext);

            if (!applicationValidationResult.IsValid)
            {
                IEnumerable<Error> errors = applicationValidationResult.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage));

                throw new ValidationErrorException(errors.ToArray());
            }

            await DbContext.Applications.AddAsync(application);


            List<Error> validationErrors = new List<Error>();

            foreach (var applicantEducation in application.ApplicantEducations)
            {
                applicantEducation.ApplicationId = application.Id;

                var result = await _applicantEducationValidator.ValidateAsync(applicantEducation);
                if (!result.IsValid)
                {
                    IEnumerable<Error> errors = result.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage));

                    validationErrors.AddRange(errors);
                }
            }

            foreach (var applicantHobby in application.ApplicantHobbies)
            {
                applicantHobby.ApplicationId = application.Id;

                var result = await _applicantHobbyValidator.ValidateAsync(applicantHobby);
                if (!result.IsValid)
                {
                    IEnumerable<Error> errors = result.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage));

                    validationErrors.AddRange(errors);
                }
            }

            foreach (var ApplicantLanguage in application.ApplicantLanguages)
            {
                ApplicantLanguage.ApplicationId = application.Id;

                var result = await _applicantLanguageValidator.ValidateAsync(ApplicantLanguage);
                if (!result.IsValid)
                {
                    IEnumerable<Error> errors = result.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage));

                    validationErrors.AddRange(errors);
                }
            }

            foreach (var applicantWorkExperience in application.ApplicantWorkExperiences)
            {
                applicantWorkExperience.ApplicationId = application.Id;

                var result = await _applicantWorkExperienceValidator.ValidateAsync(applicantWorkExperience);
                if (!result.IsValid)
                {
                    IEnumerable<Error> errors = result.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage));

                    validationErrors.AddRange(errors);
                }
            }

            if (validationErrors.Any()) throw new ValidationErrorException(validationErrors.ToArray());

            await DbContext.ApplicantEducations.AddRangeAsync(application.ApplicantEducations);
            await DbContext.ApplicantHobbies.AddRangeAsync(application.ApplicantHobbies);
            await DbContext.ApplicantLanguages.AddRangeAsync(application.ApplicantLanguages);
            await DbContext.ApplicantWorkExperiences.AddRangeAsync(application.ApplicantWorkExperiences);

            await DbContext.SaveChangesAsync();

            var applicationGetDto = Mapper.Map<ApplicationGetDto>(application);

            return new DataResult<ApplicationGetDto>(ResultStatus.Success, applicationGetDto);
        }

        public async Task<IResult> DeleteAsync(Guid applicationId)
        {
            var application = await DbContext.Applications.FirstOrDefaultAsync(x => x.Id == applicationId);

            if (application is null) throw new ArgumentNotFoundException(new Error("applicationId", "Application was not found."));

            application.IsActive = false;
            application.IsDeleted = true;
            application.DeletedDate = DateTime.Now;

            DbContext.Applications.Update(application);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "Application successfully deleted.");
        }

        public async Task<IResult> HardDeleteAsync(Guid applicationId)
        {
            var application = await DbContext.Applications.FirstOrDefaultAsync(x => x.Id == applicationId);

            if (application is null) throw new ArgumentNotFoundException(new Error("applicationId", "Application was not found."));

            DbContext.Applications.Remove(application);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "Application successfully deleted from database.");
        }
    }
}
