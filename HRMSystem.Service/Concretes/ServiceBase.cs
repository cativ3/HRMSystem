using AutoMapper;
using HRMSystem.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Concretes
{
    public class ServiceBase
    {
        protected readonly HRManagementDbContext DbContext;
        protected readonly IMapper Mapper;

        public ServiceBase(HRManagementDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }
    }
}
