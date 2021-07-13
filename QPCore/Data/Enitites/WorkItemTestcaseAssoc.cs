namespace QPCore.Data.Enitites
{
    public class WorkItemTestcaseAssoc : BaseEntity
    {
        public int WorkItemId { get; set; }

        public int TestcaseId { get; set; }

        public virtual WorkItem WorkItem { get; set; }

        public virtual TestFlow Testcase { get; set; }
    }
}