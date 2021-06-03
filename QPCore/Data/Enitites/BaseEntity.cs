using System;
using QPCore.Data.BaseEntites;

namespace QPCore.Data.Enitites
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}