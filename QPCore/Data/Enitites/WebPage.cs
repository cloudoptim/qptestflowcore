namespace QPCore.Data.Enitites
{
    public partial class WebPage : BaseGroupEntity
    {
        public bool IsActive { get; set; }
        public virtual WebPageGroup WebPageGroup { get; set; }
    }
}
