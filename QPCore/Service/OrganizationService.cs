using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Model.Organizations;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository<Organization> _repository;
        private readonly IMapper _mapper;
        public OrganizationService(IRepository<Organization> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrganizationResponse> AddAsync(CreateOrganizationRequest createOrganization, int userId)
        {
            var newOrg = _mapper.Map<Organization>(createOrganization);
            newOrg.CreatedBy = userId;
            newOrg.CreatedDate = DateTime.Now;
            newOrg = await _repository.AddAsync(newOrg);

            return _mapper.Map<OrganizationResponse>(newOrg);
        }

        public bool CheckExistingId(int id)
        {
            var query = _repository.GetQuery()
                        .Any(p => p.OrgId == id);

            return query;
        }

        public CheckUniqueResponse CheckUniqueOrgName(string name, int? orgId = null)
        {
            name = name.Trim().ToLower();

            var isExisted = false;

            if (orgId.HasValue)
            {
                isExisted = _repository.GetQuery()
                        .Any(p => p.OrgName.ToLower() == name && p.OrgId != orgId.Value);
            }
            else
            {
                isExisted = _repository.GetQuery()
                        .Any(p => p.OrgName.ToLower() == name);
            }

            var result = new CheckUniqueResponse()
            {
                IsUnique = !isExisted
            };

            return result;
        }

        public List<OrganizationResponse> GetAll()
        {
            var orgs = _repository.GetQuery()
                        .ProjectTo<OrganizationResponse>(_mapper.ConfigurationProvider)
                        .ToList();

            return orgs;
        }

        public OrganizationResponse GetById(int id)
        {
            var org = _repository.GetQuery()
                .Where(p => p.OrgId == id)
                .ProjectTo<OrganizationResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

            return org;
        }

        public async Task<OrganizationResponse> UpdateAsync(EditOrganizationRequest editOrganization, int userId)
        {
            var organization = _repository.GetQuery()
                                .Where(p => p.OrgId == editOrganization.OrgId)
                                .FirstOrDefault();

            if (organization != null)
            {
                organization.OrgName = editOrganization.OrgName;
                organization.StartDate = editOrganization.StartDate;
                organization.EndDate = editOrganization.EndDate;
                organization.UpdatedBy = userId;
                organization.UpdatedDate = DateTime.Now;

                var updatedOrganization = await _repository.UpdateAsync(organization);
                return _mapper.Map<OrganizationResponse>(updatedOrganization);
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public bool CheckCanDeleting(int id)
        {
            var canDelete = _repository.GetQuery()
                        .Any(p => p.OrgId == id && (p.Applications.Any() || p.OrgUsers.Any()));

            return !canDelete;
        }
    }
}
