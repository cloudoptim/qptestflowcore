using QPCore.Data.BaseEntites;

namespace QPCore.Data.Enitites
{
    public class BaseGroupEntity : BaseEntity, IBaseEntity, IBaseGroupEntity
    {
        public int GroupId { get; set; }
    }
}