namespace QPCore.Model.Common
{
    public class ExistedResponse
    {
        public ExistedResponse()
        {
            this.IsExisted = false;
        }
        public bool IsExisted { get; set; }
        public int? ExistedId { get; set; }
    }
}