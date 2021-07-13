using System;
using Microsoft.EntityFrameworkCore;
using QPCore.Data.Enitites;

namespace QPCore.Data.Seeds
{
    public class DefaultWorkItemTypeSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var createdDate = new DateTime(2021, 06, 10);
            modelBuilder.Entity<WorkItemType>().HasData(
                new WorkItemType()
            {
                Id = 1,
                Name = "Story",
                CreatedBy = 1,
                CreatedDate = createdDate
            },
            new WorkItemType()
            {
                Id = 2,
                Name = "Product backlog item",
                CreatedBy = 1,
                CreatedDate = createdDate
            },
            new WorkItemType()
            {
                Id = 3,
                Name = "Task",
                CreatedBy = 1,
                CreatedDate = createdDate
            },new WorkItemType()
            {
                Id = 4,
                Name = "Bug",
                CreatedBy = 1,
                CreatedDate = createdDate
            },new WorkItemType()
            {
                Id = 5,
                Name = "Issue",
                CreatedBy = 1,
                CreatedDate = createdDate
            },new WorkItemType()
            {
                Id = 6,
                Name = "Feature",
                CreatedBy = 1,
                CreatedDate = createdDate
            });
        }
    }
}