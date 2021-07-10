using System.Collections.Generic;

namespace QPCore.Data.Enitites
{
    public class WorkItem : BaseEntity
    {
        public WorkItem()
        {
            this.WorkItemTestcaseAssocs = new HashSet<WorkItemTestcaseAssoc>();
        }
        public int AzureWorkItemId { get; set; }

        public int AzureFeatureId { get; set; }

        public string FeatureName { get; set; }

        public virtual ICollection<WorkItemTestcaseAssoc> WorkItemTestcaseAssocs { get; set; }
    }
}