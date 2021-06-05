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

        public List<ExistedBulkResponse> CheckExistedBulkName(ExistedBulkNameRequest data)
        {
            data.NameList
                .AsParallel()
                .ForAll(n => n = n.Trim().ToLower());
            
            var query = this.Repository.GetQuery()
                .Where(p => p.GroupId == data.GroupId 
                    && data.NameList.Contains(p.Name.Trim().ToLower()))
                .Select(p => new 
                {
                    Name = p.Name.Trim().ToLower(),
                    Id = p.Id
                })
                .ToList();
            
            var result = new List<ExistedBulkResponse>();

            foreach (var name in data.NameList)
            {
                var lookupItem = query.FirstOrDefault(p => p.Name == name);

                var item = new ExistedBulkResponse()
                {
                    Name = name,
                    IsExisted = lookupItem != null,
                    ExistedId = lookupItem?.Id
                };

                result.Add(item);
            }

            return result;
        }
    }
}