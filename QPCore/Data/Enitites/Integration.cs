namespace QPCore.Data.Enitites
{
    public class Integration : BaseEntity
    {
        public int SourceId { get; set; }
        public int UserId { get; set; }
        public string Pat { get; set; }
        public string Organization { get; set; }
        public string Project { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }

        public virtual IntegrationSource Source { get; set; }
        public virtual OrgUser User { get; set; }

    }
}