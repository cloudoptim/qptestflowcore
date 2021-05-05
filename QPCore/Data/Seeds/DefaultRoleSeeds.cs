using Microsoft.EntityFrameworkCore;
using QPCore.Common;
using QPCore.Data.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Data.Seeds
{
    public class DefaultRoleSeeds
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Enitites.Role()
            {
                RoleId = 1,
                Rolename = "Administrator",
                RoleCode = GlobalConstants.ADMIN_ROLE_CODE,
                IsActive = true,
                IsDefault = false,
                IsSystem = true
            },
            new Enitites.Role()
            {
                RoleId = 2,
                Rolename = "User",
                RoleCode = GlobalConstants.USER_ROLE_CODE,
                IsActive = true,
                IsDefault = true,
                IsSystem = true
            });
        }
    }
}
