using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public List<WebPageItemResponse> GetByGroupId(int groupId)
        {
            var result = this.Repository.GetQuery()
                .Where(p => p.GroupId == groupId)
                .ProjectTo<WebPageItemResponse>(this.Mapper.ConfigurationProvider)
                .ToList();
            
            return result;
        }
    }
}