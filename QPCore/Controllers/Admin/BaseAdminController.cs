using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = GlobalConstants.ADMIN_ROLE_CODE)]
    public class BaseAdminController : BaseController
    {
    }
}
