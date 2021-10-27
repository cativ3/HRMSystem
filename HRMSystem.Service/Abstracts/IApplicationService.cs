using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Dtos.ApplicationDtos;
using HRMSystem.Core.Utilities.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Abstracts
{
    public interface IApplicationService
    {
        Task<IDataResult<ApplicationListDto>> GetAllAsync(
            int? workTitleId,
            int currentPage,
            int pageSize,
            bool? isActive,
            bool? isDeleted,
            ApplicationOrderBy orderBy,
            bool isAscending,
            string searchKeyword
            );

        Task<IDataResult<IEnumerable<ApplicationGetDto>>> GetAllWithoutPagingAsync(
            int? workTitleId,
            bool? isActive,
            bool? isDeleted,
            ApplicationOrderBy orderBy,
            bool isAscending,
            string searchKeyword
            );

        Task<IDataResult<ApplicationGetDto>> GetByIdAsync(Guid applicationId);

        Task<IDataResult<ApplicationGetDto>> AddAsync(ApplicationAddDto applicationAddDto);

        Task<IResult> DeleteAsync(Guid applicationId);

        Task<IResult> HardDeleteAsync(Guid applicationId);
    }
}
