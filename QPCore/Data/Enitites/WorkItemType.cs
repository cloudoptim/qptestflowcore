using System.Collections.Generic;

namespace QPCore.Data.Enitites
{
    public class WorkItemType : BaseEntity
    {
        public WorkItemType()
        {
            WorkItems = new HashSet<WorkItem>();
        }
        
        public virtual ICollection<WorkItem> WorkItems { get; set; }
    }
}