using System;

namespace QPCore.Data.BaseEntites
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        string Name { get; set; }

        int CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        int? UpdatedBy { get; set; }

        DateTime? UpdatedDate { get; set; }
    }
}