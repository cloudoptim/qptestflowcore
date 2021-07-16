using Microsoft.AspNetCore.Http;

namespace QPCore.Model.Integrations
{
    public class IntegrationResponse
    {
        public int? Id { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string Readme { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
        public string Organization { get; set; }
        public string Project { get; set; }
        public string Url { get; set; }

        public void BindObsoluteLogoPath(IHttpContextAccessor httpContextAccessor)
        {
            string host = httpContextAccessor.HttpContext.Request.Host.Value;
            string schema = httpContextAccessor.HttpContext.Request.Scheme;
            this.Logo = $"{schema}://{host}/images/icons/{this.Logo}";
        }
    }
}