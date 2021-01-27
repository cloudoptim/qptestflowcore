using DataBaseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public WebElementController(WebElementService webElementService)
        {
            _webElementService = webElementService;
        }
        // GET: api/<WebModelController>
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
        [HttpPost]
        public WebElement Post(WebElement value)
        {
            return _webElementService.AddWebElement(value);
        }

        //// PUT api/<WebModelController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, WebModel value)
        //{
        //}

        // DELETE api/<WebModelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _webElementService.deleteElement(id);
        }
    }
}
