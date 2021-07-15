using System.Collections.Generic;
using AutoMapper;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Integrations;
using QPCore.Service.Interfaces;
using System.Linq;

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
                        Pat = s.Integration == null ? string.Empty : s.Integration.Pat,
                        Project = s.Integration == null ? string.Empty : s.Integration.Project,
                        Organization = s.Integration == null ? string.Empty : s.Integration.Organization,
                        Url = s.Integration == null ? string.Empty : s.Integration.Url,
                        IsActive = s.Integration == null ? false : s.Integration.IsActive,
                    }).ToList();
            return query;
        }
    }
}