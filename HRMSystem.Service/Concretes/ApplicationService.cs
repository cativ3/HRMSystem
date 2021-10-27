using AutoMapper;
using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Dtos.ApplicationDtos;
using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.DataAccess.Contexts;
using HRMSystem.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Concretes
{
    public class ApplicationService : ServiceBase, IApplicationService
    {
        public ApplicationService(HRManagementDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public Task<IDataResult<ApplicationListDto>> GetAllAsync(int? workTitleId, int currentPage, int pageSize, bool? isActive, bool? isDeleted, ApplicationOrderBy orderBy, bool isAscending, string searchKeyword)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<IEnumerable<ApplicationGetDto>>> GetAllWithoutPagingAsync(int? workTitleId, bool? isActive, bool? isDeleted, ApplicationOrderBy orderBy, bool isAscending, string searchKeyword)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<ApplicationGetDto>> GetByIdAsync(Guid applicationId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<ApplicationGetDto>> AddAsync(ApplicationAddDto applicationAddDto)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> DeleteAsync(Guid applicationId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> HardDeleteAsync(Guid applicationId)
        {
            throw new NotImplementedException();
        }
    }
}
