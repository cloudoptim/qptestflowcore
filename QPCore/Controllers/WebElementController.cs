﻿using DataBaseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.ViewModels;
using QPCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebElementController : ControllerBase
    {
        private WebElementService _webElementService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webElementService"></param>
        public WebElementController(WebElementService webElementService)
        {
            _webElementService = webElementService;
        }
        // GET: api/<WebModelController
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       [HttpGet]
       [Route("ElementTree")]
        public IEnumerable<WebPageGroup> Get()
        {
            return _webElementService.GetWebElementTree();
        }

        //// GET api/<WebModelController>/5
        /// <summary>
        /// returns webElement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public WebElement Get(int id)
        {
            return _webElementService.GetWebElement(id);
        }

        // POST api/<WebModelController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public WebElement Post(WebElement value)
        {
            return _webElementService.AddWebElement(value);
        }
/// <summary>
/// 
/// </summary>
/// <param name="groupId"></param>
/// <returns></returns>

        [HttpGet]
        [Route("/WebPage")]
        public List<WebPageViewModel> WebPageGet(int groupId)
        {
            return _webElementService.GetWebPages(groupId);
        }

        // POST api/<WebModelController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/WebPage")]
        public WebPageViewModel WebPagePost(CreatePageViewModel value)
        {
            return _webElementService.AddWebPage(value);
        }


        // POST api/<WebModelController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/WebPage")]
        public WebPageViewModel WebPageput(UpdatePageViewModel value)
        {
            return _webElementService.UpdateWebPage(value);
        }

        [HttpPost]
        [Route("/PageGroup")]
        public WebPageGroupViewModel PageGroupPost(CreateWebPageGroupViewModel value)
        {
            return _webElementService.AddPageGroup(value);
        }


        // POST api/<WebModelController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/PageGroup")]
        public WebPageGroupViewModel PageGroupput(UpdateWebPageGroupViewModel value)
        {
            return _webElementService.UpdatePageGroup(value);
        }

        /// <returns></returns>
        [HttpDelete]
        [Route("/PageGroup")]
        public void PageGroupDelete(int id)
        {
             _webElementService.deletePageGroup(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("/PageGroup")]
        public List<WebPageGroupViewModel> PageGroupGet()
        {
            return _webElementService.GetPageGroup();
        }

        [HttpDelete]
        [Route("/WebPage")]
        public void WebPageDelete(int id)
        {
            _webElementService.deletePage(id);
        }
        //// PUT api/<WebModelController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, WebModel value)
        //{
        //}

        // DELETE api/<WebModelController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _webElementService.deleteElement(id);
        }
    }
}