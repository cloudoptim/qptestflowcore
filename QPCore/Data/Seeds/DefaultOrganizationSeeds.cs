using Microsoft.EntityFrameworkCore;
using QPCore.Common;
using QPCore.Data.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Data.Seeds
{
    public class DefaultOrganizationSeeds
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasData(new Enitites.Organization()
            {
                OrgId = GlobalConstants.DEFAUTL_ORGANIZATION_ID,
                CreatedDate = DateTime.Now,
                OrgName = "Default Organization",
            });
        }
    }
}
