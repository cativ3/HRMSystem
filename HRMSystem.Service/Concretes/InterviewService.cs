
using AutoMapper;
using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Concrete;
using HRMSystem.Core.Entities.Dtos.InterviewDtos;
using HRMSystem.Core.Utilities.Exceptions;
using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.Core.Utilities.Results.ComplexTypes;
using HRMSystem.Core.Utilities.Results.Concretes;
using HRMSystem.DataAccess.Contexts;
using HRMSystem.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Concretes
{
    public class InterviewService : ServiceBase, IInterviewService
    {
        private readonly IValidator<Interview> _validator;
        public InterviewService(HRManagementDbContext dbContext, IMapper mapper, IValidator<Interview> validator) : base(dbContext, mapper)
        {
            _validator = validator;
        }

        public async Task<IDataResult<InterviewListDto>> GetAllAsync(InterviewStatus? interviewStatus, int currentPage, int pageSize, bool? isActive, bool? isDeleted, InterviewOrderBy orderBy, bool isAscending, string searchKeyword)
        {
            IQueryable<Interview> query = DbContext.Set<Interview>().AsNoTracking();


            // Filters

            if (interviewStatus.HasValue) query = query.Where(x => x.InterviewStatus == interviewStatus);
            if (isActive.HasValue) query = query.Where(x => x.IsActive == isActive);
            if (isDeleted.HasValue) query = query.Where(x => x.IsDeleted == isDeleted);
            if (!string.IsNullOrEmpty(searchKeyword)) query = query.Where(x => x.Application.FirstName.Contains(searchKeyword) || x.Application.LastName.Contains(searchKeyword));


            // Includes

            query = query.Include(x => x.Application);
            query = query.Include(x => x.Application).ThenInclude(x => x.City);
            query = query.Include(x => x.Application).ThenInclude(x => x.Country);
            query = query.Include(x => x.Application).ThenInclude(x => x.WorkTitle);
            query = query.Include(x => x.Application).ThenInclude(x => x.ApplicantEducations);
            query = query.Include(x => x.Application).ThenInclude(x => x.ApplicantHobbies);
            query = query.Include(x => x.Application).ThenInclude(x => x.ApplicantLanguages);
            query = query.Include(x => x.Application).ThenInclude(x => x.ApplicantWorkExperiences);

            query = query.Include(x => x.InterviewerUser);
            query = query.Include(x => x.InterviewerUser).ThenInclude(x => x.City);
            query = query.Include(x => x.InterviewerUser).ThenInclude(x => x.Country);


            pageSize = pageSize > 100 ? 100 : pageSize;

            var totalCount = await query.CountAsync();

            switch (orderBy)
            {
                case InterviewOrderBy.Fullname:
                    query = isAscending
                        ? query.OrderBy(x => x.Application.FirstName)
                        : query.OrderByDescending(x => x.Application.FirstName);
                    break;
                case InterviewOrderBy.Email:
                    query = isAscending
                        ? query.OrderBy(x => x.Application.Email)
                        : query.OrderByDescending(x => x.Application.Email);
                    break;
                case InterviewOrderBy.PhoneNumber:
                    query = isAscending
                        ? query.OrderBy(x => x.Application.PhoneNumber)
                        : query.OrderByDescending(x => x.Application.PhoneNumber);
                    break;
                case InterviewOrderBy.WorkTitle:
                    query = isAscending
                        ? query.OrderBy(x => x.Application.WorkTitle.Name)
                        : query.OrderByDescending(x => x.Application.WorkTitle.Name);
                    break;
                case InterviewOrderBy.Interviewer:
                    query = isAscending
                        ? query.OrderBy(x => x.InterviewerUser.FirstName)
                        : query.OrderByDescending(x => x.InterviewerUser.FirstName);
                    break;
                default:
                    query = isAscending
                        ? query.OrderBy(x => x.MeetingDate)
                        : query.OrderByDescending(x => x.MeetingDate);
                    break;
            }

            var interviewListDto = new InterviewListDto
            {
                Interviews = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => Mapper.Map<InterviewGetDto>(x))
                    .ToListAsync(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount,
                IsAscending = isAscending
            };

            return new DataResult<InterviewListDto>(ResultStatus.Success, interviewListDto);
        }

        public async Task<IDataResult<InterviewGetDto>> GetByIdAsync(Guid interviewId)
        {
            IQueryable<Interview> query = DbContext.Set<Interview>().AsNoTracking();


            // Includes

            query = query.Include(x => x.Application);
            query = query.Include(x => x.InterviewStatus);


            var interview = await query.FirstOrDefaultAsync(x => x.Id == interviewId);

            if (interview is null) throw new ArgumentNotFoundException(new Error("interviewId", "Interview not found."));

            var interviewGetDto = Mapper.Map<InterviewGetDto>(interview);

            return new DataResult<InterviewGetDto>(ResultStatus.Success, interviewGetDto);
        }

        public async Task<IDataResult<InterviewGetDto>> AddAsync(InterviewAddDto interviewAddDto)
        {
            var isApplicationExist = await DbContext.Applications.AsNoTracking().AnyAsync(x => x.Id == interviewAddDto.ApplicationId);
            if (!isApplicationExist) throw new ArgumentNotFoundException(new Error("applicationId", "Application not found"));

            var isInterviewerUserExist = await DbContext.Users.AsNoTracking().AnyAsync(x => x.Id == interviewAddDto.InterviewerUserId);
            if (!isInterviewerUserExist) throw new ArgumentNotFoundException(new Error("interviewerUserId", "Interviewer User not found"));

            var interview = Mapper.Map<Interview>(interviewAddDto);

            interview.InterviewStatus = InterviewStatus.Pending;

            var validationResult = await _validator.ValidateAsync(interview);

            if (!validationResult.IsValid)
            {
                IEnumerable<Error> errors = validationResult.Errors.Select(x => new Error(x.PropertyName, x.ErrorMessage));

                throw new ValidationErrorException(errors.ToArray());
            }

            await DbContext.Interviews.AddAsync(interview);
            await DbContext.SaveChangesAsync();

            var interviewGetDto = Mapper.Map<InterviewGetDto>(interview);

            return new DataResult<InterviewGetDto>(ResultStatus.Success, interviewGetDto);
        }

        public async Task<IResult> UpdateAsync(InterviewUpdateDto interviewUpdateDto)
        {
            var oldInterview = await DbContext.Interviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == interviewUpdateDto.Id);

            var isApplicationExist = await DbContext.Applications.AsNoTracking().AnyAsync(x => x.Id == interviewUpdateDto.ApplicationId);
            if (!isApplicationExist) throw new ArgumentNotFoundException(new Error("applicationId", "Application not found"));

            var isInterviewerUserExist = await DbContext.Users.AsNoTracking().AnyAsync(x => x.Id == interviewUpdateDto.InterviewerUserId);
            if (!isInterviewerUserExist) throw new ArgumentNotFoundException(new Error("interviewerUserId", "Interviewer User not found"));

            var updatedInterview = Mapper.Map<InterviewUpdateDto, Interview>(interviewUpdateDto, oldInterview);

            var validationResult = await _validator.ValidateAsync(updatedInterview);

            if (!validationResult.IsValid)
            {
                IEnumerable<Error> errors = validationResult.Errors.Select(x => new Error(x.PropertyName, x.ErrorMessage));

                throw new ValidationErrorException(errors.ToArray());
            }

            DbContext.Interviews.Update(updatedInterview);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "Interview updated.");
        }

        public async Task<IResult> DeleteAsync(Guid interviewId)
        {
            var interview = await DbContext.Interviews.FirstOrDefaultAsync(x => x.Id == interviewId);
            if (interview is null) throw new ArgumentNotFoundException(new Error("interviewId", "Interview not found."));

            interview.IsActive = false;
            interview.IsDeleted = true;
            interview.DeletedDate = DateTime.Now;

            DbContext.Interviews.Update(interview);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "Interview successfully deleted.");
        }

        public async Task<IResult> HardDeleteAsync(Guid interviewId)
        {
            var interview = await DbContext.Interviews.FirstOrDefaultAsync(x => x.Id == interviewId);
            if (interview is null) throw new ArgumentNotFoundException(new Error("interviewId", "Interview not found."));

            DbContext.Interviews.Remove(interview);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "Interview successfully deleted from database.");
        }
    }
}
