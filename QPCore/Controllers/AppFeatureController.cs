using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QPCore.Model.DataBaseModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using QPCore.Model.DataBaseModel;
using QPCore.Service;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AnyOrignPolicy")]
    [ApiController]
    public class AppFeatureController : ControllerBase
    {
        private FeatureAppService _featureService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="featureService"></param>
        public AppFeatureController(FeatureAppService featureService)
        {
            _featureService = featureService;
        }

        // GET: api/<AppFeatureController>
        [HttpGet]
        public List<AppFeature> Get()
        {
            List<AppFeature> appfeatures = _featureService.GetAppFeature(); 
            return appfeatures;
        }

        // POST api/<AppFeatureController>
        [HttpPost]
        public AppFeatureView Post(AppFeatureView feature)
        {
            AppFeatureView appfeature = _featureService.CreateFeature(feature);
            return appfeature;
        }

        // PUT api/<AppFeatureController>/5
        [HttpPut("{id}")]
        public AppFeatureView Put(int id, AppFeatureView feature)
        {
            feature.AppFeatureId = id;
            AppFeatureView appfeature = _featureService.UpdateFeature(feature);
            return appfeature;
        }

        // DELETE api/<AppFeatureController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _featureService.DeleteFeature(id);
        }
    }
}
