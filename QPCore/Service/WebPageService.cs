using System.Linq;
using AutoMapper;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Model.WebPages;
using QPCore.Service.Interfaces;

namespace QPCore.Service
{
    public class WebPageService : BaseGroupService<WebPage, WebPageItemResponse, CreateWebPageRequest, EditWebPageRequest>, IWebPageService
    {
        public WebPageService(IBaseRepository<WebPage> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}