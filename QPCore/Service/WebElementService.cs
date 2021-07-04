using AutoMapper;
using DataBaseModel;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
using QPCore.Data;
using QPCore.Model.DataBaseModel;
using QPCore.Model.ViewModels;
using QPCore.Model.WebElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QPCore.Service
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WebElementService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly PostgresDataBase _postgresDataBase;
        private readonly IRepository<QPCore.Data.Enitites.WebElement> _repository;

        private readonly IRepository<QPCore.Data.Enitites.WebPageGroup> _webPageGroupRepository;

        IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        /// <param name="mapper"></param>
        /// <param name="repository"></param>
        /// <param name="compositeRepository"></param>
        public WebElementService(PostgresDataBase postgresDataBase, IMapper mapper, IRepository<QPCore.Data.Enitites.WebElement> repository,
            IRepository<QPCore.Data.Enitites.WebPageGroup> webPageGroupRepository)
        {
            _postgresDataBase = postgresDataBase;
            _mapper = mapper;
            _repository = repository;
            _webPageGroupRepository = webPageGroupRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WebPageGroup> GetWebElementTree()
        {
            var _data = _postgresDataBase.Procedure("getwebelementtree").ToList().FirstOrDefault();
            List<WebPageGroup> webModel = JsonConvert.DeserializeObject<List<WebPageGroup>>(_data.getwebelementtree);

            return webModel;
        }

        public List<WebPageGroupTree> GetHierarchyTree()
        {
            var tree = this._webPageGroupRepository.GetQuery()
                .Select(p => new WebPageGroupTree
                {
                    Id = p.Id,
                    GroupName = p.Name,
                    WebPages = p.WebPages
                        .OrderBy(wp => wp.Name)
                        .Select(wp => new WebPageTreeItem
                        {
                            PageId = wp.Id,
                            GroupId = wp.GroupId,
                            PageName = wp.Name,
                            CompositeElements = wp.CompositeWebElements // Composite Web Element
                            .Where(ce => ce.ParentId == null)
                            .Select(ce => new ElementTreeItem
                            {
                                ElementId = ce.Id,
                                ElementName = ce.Name,
                                PageId = ce.GroupId,
                                GroupId = p.Id,
                                IsComposite = true
                            }),
                            WebElements = wp.WebElements.Select(we => new ElementTreeItem
                            {
                                ElementId = we.Elementid,
                                ElementName = we.Elementaliasname,
                                PageId = we.Pageid,
                                GroupId = p.Id,
                                IsComposite = false
                            })
                        })
                })
                .OrderBy(p => p.GroupName)
                .ToList();

            return tree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WebElement GetWebElement(int id)
        {
            var json = _postgresDataBase.Procedure("getElement", new { elementId = id }).ToList().FirstOrDefault();
            WebElement webModel = JsonConvert.DeserializeObject<WebElement>(json.getelement);

            return webModel;
        }

        internal WebElement AddWebElement(WebElement value)
        {
            //addorUpdateWebElement
            value.screenshot = value.screenshot == null ? string.Empty : value.screenshot;
            var json = _postgresDataBase.Procedure("addorUpdateWebElement", new
            {

                elementid = value.elementid,
                elementaliasname = value.elementaliasname ?? string.Empty,
                elementtype = value.elementtype ?? string.Empty,
                itype = value.itype ?? string.Empty,
                ivalue = value.ivalue ?? string.Empty,
                framenavigation = value.framenavigation ?? string.Empty,
                command = value.command ?? string.Empty,
                locationpath = value.locationpath ?? string.Empty,
                screenshot = value.screenshot ?? string.Empty,
                elementparentid = value.elementparentid ?? 0,
                applicationsection = value.applicationsection ?? string.Empty,
                value = value.value ?? string.Empty,
                pageid = value.pageid

            }).ToList().FirstOrDefault();

            WebElement webModel = JsonConvert.DeserializeObject<WebElement>(json.addorupdatewebelement);

            return webModel;
        }

        internal WebElement UpdateWebElement(EditWebElementRequest value)
        {
            var data = _mapper.Map<WebElement>(value);
            return AddWebElement(data);
        }

        internal WebPageViewModel AddWebPage(CreatePageViewModel value)
        {
            //addorUpdateWebElement


            var json = _postgresDataBase.Procedure("CreateOrUpdateWebPage", new
            {

                pageid = 0,
                pagename = value.pageName,
                groupid = value.groupId,
                userid = value.createdBy

            }).ToList().FirstOrDefault();
            WebPageViewModel WebPage = JsonConvert.DeserializeObject<WebPageViewModel>(json.createorupdatewebpage);


            return WebPage;
        }

        internal WebPageViewModel UpdateWebPage(UpdatePageViewModel value)
        {
            //addorUpdateWebElement


            var json = _postgresDataBase.Procedure("CreateOrUpdateWebPage", new
            {

                pageid = value.pageId,
                pagename = value.pageName,
                groupid = value.groupId,
                userid = value.createdBy

            }).ToList().FirstOrDefault();

            WebPageViewModel WebPage = JsonConvert.DeserializeObject<WebPageViewModel>(json.createorupdatewebpage);


            return WebPage;
        }

        internal List<WebPageGroupViewModel> GetPageGroup()
        {
            var json = _postgresDataBase.Multiple<WebPageGroupViewModel>("Select we.\"page_group_id\" as \"id\", we.\"group_name\" as \"groupname\", we.\"version_id\" as \"versionid\" from  public.\"WebPageGroup\" we").ToList();

            return json;
        }

        internal List<WebPageViewModel> GetWebPages(int groupid)
        {
            //addorUpdateWebElement

            var json = _postgresDataBase.Multiple<WebPageViewModel>("Select we.\"page_id\" as \"pageId\", we.\"page_name\" as \"pageName\", we.\"is_active\" as \"isActive\", we.\"group_id\" as \"groupid\" from  public.\"WebPage\" we where  we.\"group_id\" ={0} ", groupid).ToList();

            return json;
        }
        internal void deletePageGroup(int id)
        {

            _postgresDataBase.Procedure("deletePageGroup", new { elementid = id }).ToList().FirstOrDefault();
        }

        internal void deletePage(int id)
        {
            _postgresDataBase.Procedure("deletePage", new { elementid = id }).ToList().FirstOrDefault();
        }

        internal WebPageGroupViewModel AddPageGroup(CreateWebPageGroupViewModel value)
        {
            //addorUpdateWebElement


            var json = _postgresDataBase.Procedure("CreateOrUpdatePageGroup", new
            {

                groupid = 0,
                groupname = value.groupname,
                userid = value.createdby,
                versionid = value.versionid
            }).ToList().FirstOrDefault();



            WebPageGroupViewModel WebPageGroupViewModel = JsonConvert.DeserializeObject<WebPageGroupViewModel>(json.createorupdatepagegroup);


            return WebPageGroupViewModel;
        }

        internal WebPageGroupViewModel UpdatePageGroup(UpdateWebPageGroupViewModel value)
        {
            //addorUpdateWebElement


            var json = _postgresDataBase.Procedure("CreateOrUpdatePageGroup", new
            {

                groupid = value.id,
                groupname = value.groupname,
                userid = value.createdby,
                versionid = value.versionid
            }).ToList().FirstOrDefault();



            WebPageGroupViewModel WebPageGroupViewModel = JsonConvert.DeserializeObject<WebPageGroupViewModel>(json.createorupdatepagegroup);


            return WebPageGroupViewModel;
        }
        //deleteElement
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>

        public void deleteElement(int id)
        {
            _postgresDataBase.Procedure("deleteElement", new { elementid = id }).ToList().FirstOrDefault();
        }

        public List<CheckingWebElementItem> CheckingWebElements(CheckingWebElementDTO webElementDTO)
        {
            string elements = JsonConvert.SerializeObject(webElementDTO.Elements);
            List<Npgsql.NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("p_elements", elements, NpgsqlDbType.Json));
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("p_pageId", webElementDTO.PageId, NpgsqlDbType.Integer));

            var data = _postgresDataBase.ProcedureJson("checkingwebelementnames", npgsqlParameters).ToList();
            var result = JsonConvert.DeserializeObject<List<CheckingWebElementItem>>(JsonConvert.SerializeObject(data));
            return result;
        }

        public bool CheckExistedId(int elementId)
        {
            var isExisted = _repository.GetQuery()
                .Any(p => p.Elementid == elementId);

            return isExisted;
        }

        public async Task<List<WebElementItem>> UpsertBulkAsync(List<WebElement> elements, int userId)
        {
            var result = new List<WebElementItem>();

            foreach (var element in elements)
            {
                var item = await this.UpsertAsync(element, userId);
                result.Add(new WebElementItem()
                {
                    ElementId = item.Elementid,
                    ElementAliasName = item.Elementaliasname
                });
            }
            return result;
        }

        private async Task<QPCore.Data.Enitites.WebElement> UpsertAsync(WebElement element, int userId)
        {
            var item = this._repository.GetQuery()
                .FirstOrDefault(p => p.Elementaliasname.Trim().ToLower() == element.elementaliasname.Trim().ToLower() &&
                    p.Pageid == element.pageid);

            if (item != null) // Update current elemment
            {
                item.Elementtype = element.elementtype ?? string.Empty;
                item.Itype = element.itype ?? string.Empty;
                item.Framenavigation = element.framenavigation ?? string.Empty;
                item.Command = element.command;
                item.Locationpath = element.locationpath ?? string.Empty;
                item.Screenshot = element.screenshot ?? string.Empty;
                item.Elementparentid = element.pageid;
                item.Applicationsection = element.applicationsection ?? string.Empty;
                item.Value = element.value ?? string.Empty;
                item.IValue = element.ivalue ?? string.Empty;
                item.UpdatedBy = userId;
                item.UpdatedDate = DateTime.Now;

                item = await this._repository.UpdateAsync(item);
            }
            else // insert new one
            {
                item = new QPCore.Data.Enitites.WebElement()
                {
                    Elementaliasname = element.elementaliasname.Trim(),
                    Elementtype = element.elementtype ?? string.Empty,
                    Itype = element.itype ?? string.Empty,
                    Framenavigation = element.framenavigation ?? string.Empty,
                    Command = element.command,
                    Locationpath = element.locationpath ?? string.Empty,
                    Screenshot = element.screenshot ?? string.Empty,
                    Elementparentid = element.pageid,
                    Applicationsection = element.applicationsection ?? string.Empty,
                    Value = element.value ?? string.Empty,
                    IValue = element.ivalue ?? string.Empty,
                    Pageid = element.pageid,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };

                item = await this._repository.AddAsync(item);
            }

            return item;
        }
    }
}
