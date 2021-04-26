using QPCore.Model.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IOrganizationService
    {
        OrganizationResponse GetById(int id);
    }
}
