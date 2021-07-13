using System.Collections.Generic;

namespace QPCore.Model.Common
{
    public class PaginationResponse<T> where T : class
    {
        public PaginationResponse()
        {
            this.Items = new List<T>();
        }
        public List<T> Items { get; set; }
        public int Total { get; set; }
    }
}