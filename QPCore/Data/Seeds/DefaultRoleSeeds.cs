using Microsoft.EntityFrameworkCore;
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
                Roleid = 1,
                Rolename = "Administrator",
                IsActive = true,
                IsDefault = false,
                IsSystem = true
            },
            new Enitites.Role()
            {
                Roleid = 2,
                Rolename = "User",
                IsActive = true,
                IsDefault = true,
                IsSystem = true
            });
        }
    }
}
