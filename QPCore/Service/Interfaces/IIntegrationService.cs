using QPCore.Data.Enitites;
using QPCore.Model.Integrations;
using System.Collections.Generic;

namespace QPCore.Service.Interfaces
{
    public interface IIntegrationService  : IBaseService<Integration, IntegrationResponse, CreateIntegrationRequest, EditIntegrationRequest>
    {
        List<IntegrationResponse> GetAllSources(int userId);
    }
}