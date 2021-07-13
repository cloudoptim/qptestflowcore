using Microsoft.EntityFrameworkCore;
using QPCore.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Data
{
    public partial class QPContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            DefaultOrganizationSeeds.Seed(modelBuilder);
            DefaultRoleSeeds.Seed(modelBuilder);
            DefaultWorkItemTypeSeed.Seed(modelBuilder);
        }
    }
}
