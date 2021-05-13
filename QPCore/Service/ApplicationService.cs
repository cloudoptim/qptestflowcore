using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Applications;
using QPCore.Model.Common;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<Application> _repository;    
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IRepository<AppUser> appUserRepository,
            IRepository<Application> applicationRepository,
            IMapper mapper)
        {
            _repository = applicationRepository;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public bool CheckCanDeleting(int id)
        {
            var canDelete = _repository.GetQuery()
                        .Any(p => p.ApplicationId == id && (p.AppUsers.Any()
                                                                || p.ConfigTestFlowConfigs.Any()
                                                                || p.RunTestBatches.Any()
                                                                || p.StepGlossaries.Any()
                                                                || p.TestFlowCategories.Any()
                                                                || p.TestFlows.Any()
                                                                || p.WebCommands.Any()));

            return !canDelete;
        }

        public bool CheckExistingId(int id)
        {
            var query = _repository.GetQuery()
                       .Any(p => p.ClientId == id);

            return query;
        }

        public CheckUniqueResponse CheckUniqueApplicationName(string name, int? clientId = null)
        {
            name = name.Trim().ToLower();

            var isExisted = false;

            if (clientId.HasValue)
            {
                isExisted = _repository.GetQuery()
                        .Any(p => p.ApplicationName.ToLower() == name && p.ClientId != clientId.Value);
            }
            else
            {
                isExisted = _repository.GetQuery()
                        .Any(p => p.ApplicationName.ToLower() == name);
            }

            var result = new CheckUniqueResponse()
            {
                IsUnique = !isExisted
            };

            return result;
        }

        public async Task<ApplicationResponse> CreateAsync(CreateApplicationRequest createApplicationRequest, int userId)
        {
            var newApplication = _mapper.Map<Application>(createApplicationRequest);
            newApplication.CreatedBy = userId;
            newApplication.CreatedDate = DateTime.Now;
            newApplication.IsActive = true;
            newApplication = await _repository.AddAsync(newApplication);

            return GetById(newApplication.ClientId);
        }

        public async Task DeleteAsync(int id)
        {
           await _repository.DeleteAsync(id);
        }

        public List<ApplicationResponse> GetAll()
        {
            var orgs = _repository.GetQuery()
                        .ProjectTo<ApplicationResponse>(_mapper.ConfigurationProvider)
                        .ToList();

            return orgs;
        }

        public ApplicationResponse GetById(int clientId)
        {
            var query = _repository.GetQuery()
                        .Where(p => p.ClientId == clientId)
                        .ProjectTo<ApplicationResponse>(_mapper.ConfigurationProvider);
            return query.FirstOrDefault();
        }

        public List<ApplicationResponse> GetUserApplications(int userId)
        {
            var query = _appUserRepository.GetQuery()
                            .Where(p => p.Userid == userId)
                            .Select(p => new ApplicationResponse
                            {
                                ClientId = p.Application.ClientId,
                                ApplicationId = p.Application.ApplicationId,
                                ApplicationName = p.Application.ApplicationName,
                                IsActive = p.Application.IsActive,
                            })
                            .ToList();

            return query;
        }

        public async Task<ApplicationResponse> UpdateAsync(EditApplicationRequest editApplicationRequest, int userId)
        {
            var application = _repository.GetQuery()
                                .Where(p => p.ClientId == editApplicationRequest.ClientId)
                                .FirstOrDefault();


            if (application != null)
            {
                application.OrgId = editApplicationRequest.OrgId;
                application.ApplicationName = editApplicationRequest.ApplicationName;
                application.IsActive = editApplicationRequest.IsActive;
                application.UpdatedBy = userId;
                application.UpdatedDate = DateTime.Now;

                var updatedApplication = await _repository.UpdateAsync(application);
                return GetById(updatedApplication.ClientId);
            }

            return null;
        }
    }
}
