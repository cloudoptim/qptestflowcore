using QPCore.Model.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IRoleService
    {
        List<RoleResponse> GetAll();
    }
}
