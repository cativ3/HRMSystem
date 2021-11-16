using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.Core.Entities.Dtos.InterviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMSystem.Core.Entities.ComplexTypes;

namespace HRMSystem.Service.Abstracts
{
    public interface IInterviewService
    {
        Task<IDataResult<InterviewListDto>> GetAllAsync(
            InterviewStatus? interviewStatus,
            int currentPage,
            int pageSize,
            bool? isActive,
            bool? isDeleted,
            InterviewOrderBy orderBy,
            bool isAscending,
            string searchKeyword);

        Task<IDataResult<InterviewGetDto>> GetByIdAsync(Guid interviewId);

        Task<IDataResult<InterviewGetDto>> AddAsync(InterviewAddDto interviewAddDto);

        Task<IResult> UpdateAsync(InterviewUpdateDto interviewUpdateDto);

        Task<IResult> DeleteAsync(Guid interviewId);

        Task<IResult> HardDeleteAsync(Guid interviewId);
    }
}
