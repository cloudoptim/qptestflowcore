using System.Collections.Generic;
using AutoMapper;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Integrations;
using QPCore.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class IntegrationService : BaseService<Integration, IntegrationResponse, CreateIntegrationRequest, EditIntegrationRequest>, IIntegrationService
    {
        private readonly IRepository<IntegrationSource> _sourceRepository;
        public IntegrationService(IBaseRepository<Integration> repository, IMapper mapper,
                IRepository<IntegrationSource> sourceRepository) : base(repository, mapper)
        {
            _sourceRepository = sourceRepository;
        }

        public List<IntegrationResponse> GetAllSources(int userId)
        {
            var query = this._sourceRepository.GetQuery()
                .GroupJoin(this.Repository.GetQuery().Where(p => p.UserId == userId), l => l.Id, r => r.SourceId, (l, r) => new { Source = l, Integration = r })
                .SelectMany(s => s.Integration.DefaultIfEmpty(),
                    (l, r) => new { Source = l.Source, Integration = r })
                    .Select(s => new IntegrationResponse()
                    {
                        Id = s.Integration == null ? (int?)null : s.Integration.Id,
                        Logo = s.Source.Logo,
                        SourceName = s.Source.Name,
                        Readme = s.Source.Readme,
                        SourceId = s.Source.Id,
                        Project = s.Integration == null ? string.Empty : s.Integration.Project,
                        Organization = s.Integration == null ? string.Empty : s.Integration.Organization,
                        Url = s.Integration == null ? string.Empty : s.Integration.Url,
                        IsActive = s.Integration == null ? false : s.Integration.IsActive
                    }).ToList();
            return query;
        }

        public override async Task<IntegrationResponse> EditAsync(EditIntegrationRequest entity, int userId)
        {
            var updateEntity = this.Repository.GetQuery().FirstOrDefault(p => p.Id == entity.Id);
            if (updateEntity != null)
            {
                updateEntity.Organization = entity.Organization;
                updateEntity.Project = entity.Project;
                updateEntity.Url = entity.Url;
                updateEntity.UpdatedBy = userId;
                updateEntity.UpdatedDate = System.DateTime.Now;

                var result = await this.Repository.UpdateAsync(updateEntity);
            }
            
            return GetById(entity.Id);
        }

        public async Task<IntegrationResponse> EditPatAsync(EditPatRequest entity, int userId)
        {
            var updateEntity = this.Repository.GetQuery().FirstOrDefault(p => p.Id == entity.Id);
            if (updateEntity != null)
            {
                updateEntity.Pat = entity.Pat;
                updateEntity.UpdatedBy = userId;
                updateEntity.UpdatedDate = System.DateTime.Now;

                var result = await this.Repository.UpdateAsync(updateEntity);
            }
            
            return GetById(entity.Id);
        }

        public async Task<IntegrationResponse> EditActivationAsync(EditActivateRequest entity, int userId)
        {
            var updateEntity = this.Repository.GetQuery().FirstOrDefault(p => p.Id == entity.Id);
            if (updateEntity != null)
            {
                updateEntity.IsActive = entity.IsActive;
                updateEntity.UpdatedBy = userId;
                updateEntity.UpdatedDate = System.DateTime.Now;

                var result = await this.Repository.UpdateAsync(updateEntity);
            }
            
            return GetById(entity.Id);
        }

        public bool CheckExistedAssignment(int sourceId, int userId, int? id = null)
        {
            var query = this.Repository.GetQuery()
                .Any(p => p.UserId == userId &&
                    p.SourceId == sourceId &&
                    (id == null || p.Id != id));
            
            return query;
        }
    }
}