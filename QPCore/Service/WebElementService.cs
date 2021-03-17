using DataBaseModel;
using Newtonsoft.Json;
using QPCore.DAO;
using QPCore.Model.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace QPCore.Service
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WebElementService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        PostgresDataBase _postgresDataBase;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        public WebElementService(PostgresDataBase postgresDataBase)
        {
            _postgresDataBase = postgresDataBase;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WebPageGroup> GetWebElementTree()
        {
            var _data = _postgresDataBase.Procedure("getwebelementtree").ToList().FirstOrDefault();
             List<WebPageGroup>  webModel = JsonConvert.DeserializeObject<List<WebPageGroup>>(_data.getwebelementtree);

            return webModel;
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
           var json = _postgresDataBase.Procedure("addorUpdateWebElement", new {

                elementid = value.elementid,
                elementaliasname = value.elementaliasname,
                elementtype = value.elementtype,
                itype = value.itype,
                ivalue = value.ivalue,
                framenavigation = value.framenavigation,
                command = value.command,
                locationpath = value.locationpath,
                screenshot = value.screenshot,
                elementparentid = value.elementparentid,
                applicationsection = value.applicationsection,
                value = value.value,
                pageid =  value.pageid

            }).ToList().FirstOrDefault();

            WebElement webModel = JsonConvert.DeserializeObject<WebElement>(json.addorupdatewebelement);

            return webModel;
        }

        internal WebPageViewModel AddWebPage(CreatePageViewModel value)
        {
            //addorUpdateWebElement

           
            var json = _postgresDataBase.Procedure("CreateOrUpdateWebPage", new
            {

                pageid = 0,
                pagename = value.pageName,
                groupid =  value.groupId,
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
            var json = _postgresDataBase.Multiple<WebPageGroupViewModel>("Select we.* from  public.\"WebPageGroup\" we").ToList();



            return json;
        }

        internal List<WebPageViewModel> GetWebPages(int groupid)
        {
            //addorUpdateWebElement


            var json = _postgresDataBase.Multiple<WebPageViewModel>("Select we.* from  public.\"WebPage\" we where  we.\"groupid\" ={0} ",groupid).ToList();



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
                versionid =  value.versionid
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
    }
}
