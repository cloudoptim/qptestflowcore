using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Applications;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IRepository<AppUser> appUserRepository,
            IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public List<ApplicationResponse> GetUserApplications(int userId)
        {
            var query = _appUserRepository.GetQuery()
                            .Where(p => p.Userid == userId)
                            .Select(p => new ApplicationResponse
                            {
                                ClientId = p.Application.ClientId,
                                ApplicationName = p.Application.ApplicationName,
                                IsActive = p.Application.IsActive,
                                Enable = p.Enabled[0]
                            })
                            .ToList();

            return query;
        }
    }
}
