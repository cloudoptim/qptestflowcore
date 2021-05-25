using DataBaseModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Produces("application/json")]
    //[EnableCors("AnyOrignPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WebModelController : BaseController
    {
        private WebModelService _webModelService;

        public WebModelController(WebModelService webModelService)
        {
            _webModelService = webModelService;
        }
        // GET: api/<WebModelController>
        [HttpGet]
        public IEnumerable<WebModel> Get()
        {
            return _webModelService.GetWebModels();
        }

        // GET api/<WebModelController>/5
        [HttpGet("{id}")]
        public WebModel Get(int id)
        {
            return _webModelService.GetWebModel(id);
        }

        // POST api/<WebModelController>
        [HttpPost]
        public WebModel Post(WebModel value)
        {
           return  _webModelService.AddModel(value);
        }

        // PUT api/<WebModelController>/5
        [HttpPut("{id}")]
        public void Put(int id, WebModel value)
        {
        }

        // DELETE api/<WebModelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _webModelService.deleteModel(id);
        }
    }
}
