using Microsoft.EntityFrameworkCore;
using QPCore.Common;
using QPCore.Data.Enitites;
using System;

namespace QPCore.Data.Seeds
{
    public class DefaultOrganizationSeeds
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var createdDate = new DateTime(2021, 06, 10);
            modelBuilder.Entity<Organization>().HasData(new Enitites.Organization()
            {
                OrgId = GlobalConstants.DEFAUTL_ORGANIZATION_ID,
                CreatedDate = createdDate,
                OrgName = "Default Organization",
            });
        }
    }
}
