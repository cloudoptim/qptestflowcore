﻿using DataBaseModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.ViewModels;
using QPCore.Model.WebElement;
using QPCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using QPCore.Model.DataBaseModel;
using QPCore.Model.Common;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("AnyOrignPolicy")]
    [ApiController]
    [Authorize]
    public class WebElementController : BaseController
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
        // GET: api/ElementTree
        /// <summary>
        /// Obsolete: We should change to new endpoint api/webelement/webelementtree
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ElementTree")]
        public IEnumerable<WebPageGroup> Get()
        {
            return _webElementService.GetWebElementTree();
        }

        /// <summary>
        /// Get web element hierarchy tree
        /// </summary>
        /// <returns></returns>
        [HttpGet("webelementtree")]
        public ActionResult<IEnumerable<WebPageGroupTree>> GetHierachyTree()
        {
            var treeView = _webElementService.GetHierarchyTree();
            return Ok(treeView);
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
        public ActionResult<WebElement> Post(WebElement value)
        {
            var result = _webElementService.CheckingWebElements(new CheckingWebElementDTO()
            {
                PageId = value.pageid,
                Elements = new List<WebElementItem>()
                {
                    new WebElementItem()
                    {
                        ElementId = null, 
                        ElementAliasName = value.elementaliasname
                    }
                }
            });

            if (result.FirstOrDefault().IsExisted)
            {
                // Checking if it's existing element which is same all properties

                return BadRequest(new BadRequestResponse() 
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, value.elementaliasname)
                });
            }

            return _webElementService.AddWebElement(value);
        }

        /// <summary>
        /// Edit web element
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<WebElement> Put(EditWebElementRequest value)
        {
            var isExisted = _webElementService.CheckExistedId(value.elementid);
            if (!isExisted)
            {
                return BadRequest(new BadRequestResponse() 
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, value.elementid)
                });
            }

            var result = _webElementService.CheckingWebElements(new CheckingWebElementDTO()
            {
                PageId = value.pageid,
                Elements = new List<WebElementItem>()
                {
                    new WebElementItem()
                    {
                        ElementId = value.elementid, 
                        ElementAliasName = value.elementaliasname
                    }
                }
            });

            if (result.FirstOrDefault().IsExisted)
            {
                return BadRequest(new BadRequestResponse() 
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, value.elementaliasname)
                });
            }

            return _webElementService.UpdateWebElement(value);
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

        /// <summary>
        /// API to  check web element name , If element name exists return true and else return false 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("checkingelements")]
        public List<CheckingWebElementItem> CheckingElements(CheckingWebElementDTO data)
        {
            var result = _webElementService.CheckingWebElements(data);
            return result;
        }
        
        [HttpPost]
        [Route("bulk")]
        public async Task<ActionResult<List<WebElementItem>>> UpsertBulkElements(List<WebElement> elements)
        {
            var result = await _webElementService.UpsertBulkAsync(elements, Account.UserId);

            return Ok(result);
        }
    }
}
