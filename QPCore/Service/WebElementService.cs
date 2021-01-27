using AutomationAssistant.Models.AppConfig;
using DataBaseModel;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class WebElementService
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
            WebElement webModel = JsonConvert.DeserializeObject<WebElement>(json.getmodeljson);

            return webModel;
        }

        internal WebElement AddWebElement(WebElement value)
        {
            //addorUpdateWebElement

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



            throw new NotImplementedException();
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
