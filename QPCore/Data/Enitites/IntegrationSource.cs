using System.Collections.Generic;

namespace QPCore.Data.Enitites
{
    public class IntegrationSource : BaseEntity
    {
        public IntegrationSource()
        {
            this.Integrations = new HashSet<Integration>();
        }
        public string Logo { get; set; }

        public string Readme { get; set; }

        public virtual ICollection<Integration> Integrations { get; set; }
    }
}