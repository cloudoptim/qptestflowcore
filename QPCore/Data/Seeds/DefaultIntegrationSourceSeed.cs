using Microsoft.EntityFrameworkCore;
using QPCore.Common;
using QPCore.Data.Enitites;
using System;

namespace QPCore.Data.Seeds
{
    public class DefaultIntegrationSourceSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var createdDate = new DateTime(2021, 06, 10);
            modelBuilder.Entity<IntegrationSource>().HasData(
                new Enitites.IntegrationSource()
                {
                    Id = 1,
                    Name = "Azure",
                    Logo = "azure.png",
                    CreatedDate = createdDate,
                    Readme = "Readme",
                    CreatedBy = 1,
                },
                new Enitites.IntegrationSource()
                {
                    Id = 2,
                    Name = "Jira",
                    Logo = "jira.png",
                    CreatedDate = createdDate,
                    Readme = "Readme",
                    CreatedBy = 1,
                },
                new Enitites.IntegrationSource()
                {
                    Id = 3,
                    Name = "AGM",
                    Logo = "agm.jpeg",
                    CreatedDate = createdDate,
                    Readme = "Readme",
                    CreatedBy = 1,
                },
                new Enitites.IntegrationSource()
                {
                    Id = 4,
                    Name = "Slack",
                    Logo = "slack.png",
                    CreatedDate = createdDate,
                    Readme = "Readme",
                    CreatedBy = 1,
                },
                new Enitites.IntegrationSource()
                {
                    Id = 5,
                    Name = "Jenkins",
                    Logo = "jenkins.png",
                    CreatedDate = createdDate,
                    Readme = "Readme",
                    CreatedBy = 1,
                }
            );
        }
    }
}