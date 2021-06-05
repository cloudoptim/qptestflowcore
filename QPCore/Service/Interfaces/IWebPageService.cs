using System.Collections.Generic;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Model.WebPages;

namespace QPCore.Service.Interfaces
{
    public interface IWebPageService : IBaseGroupService<WebPage, WebPageItemResponse, CreateWebPageRequest, EditWebPageRequest>
    {
        List<WebPageItemResponse> GetByGroupId(int groupId);

        List<ExistedBulkResponse> CheckExistedBulkName(ExistedBulkNameRequest data);
    }
}