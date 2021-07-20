using QPCore.Data.Enitites;
using QPCore.Model.Integrations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IIntegrationService  : IBaseService<Integration, IntegrationResponse, CreateIntegrationRequest, EditIntegrationRequest>
    {
        List<IntegrationResponse> GetAllSources(int userId);
        Task<IntegrationResponse> EditPatAsync(EditPatRequest entity, int userId);
        Task<IntegrationResponse> EditActivationAsync(EditActivateRequest entity, int userId);
        bool CheckExistedAssignment(int sourceId, int userId, int? id = null);
    }
}