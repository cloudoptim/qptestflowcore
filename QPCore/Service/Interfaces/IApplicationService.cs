using QPCore.Model.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IApplicationService
    {
        List<ApplicationResponse> GetUserApplications(int userId);
    }
}
