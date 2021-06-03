using AutoMapper;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.WebPageGroups;
using QPCore.Service.Interfaces;

namespace QPCore.Service
{
    public class WebPageGroupService : BaseService<WebPageGroup, PageGroupItemResponse, CreatePageGroupRequest, EditPageGroupRequest>, IWebPageGroupService
    {
        public WebPageGroupService(IBaseRepository<WebPageGroup> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}