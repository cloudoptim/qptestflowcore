﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using QPCore.Model.DataBaseModel;
using Microsoft.AspNetCore.Cors;
using QPCore.Service;
using QPCore.Model.Common;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("AnyOrignPolicy")]
    [ApiController]
    [Authorize]
    public class AppFeatureController : BaseController
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

        [HttpGet("{id}/steps")]
        public List<Steps> Get(int id)
        {
            List<Steps> steps = _featureService.GetAppFeatureSteps(id);
            return steps;
        }

        // POST api/<AppFeatureController>
        [HttpPost]
        public ActionResult<AppFeatureView> Post(AppFeatureView feature)
        {
            var isExitedName = _featureService.CheckFeatureNameExisted(feature.FeatureName, feature.ParentFeatureId);
            if (isExitedName)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, feature.FeatureName)
                });
            }
            if (feature.ParentFeatureId.HasValue)
            {
                var isExistedParentId = _featureService.CheckExistedId(feature.ParentFeatureId.Value);
                if (!isExistedParentId)
                {
                    return BadRequest(new BadRequestResponse()
                    {
                        Message = string.Format("Parent Id: " + CommonMessageList.NOT_FOUND_THE_ID, feature.ParentFeatureId.Value)
                    });
                }
            }

            AppFeatureView appfeature = _featureService.CreateFeature(feature);
            return Ok(appfeature);
        }

        // PUT api/<AppFeatureController>/5
        [HttpPut("{id}")]
        public ActionResult<AppFeatureView> Put(int id, AppFeatureView feature)
        {
            feature.AppFeatureId = id;

            var isExistedId = _featureService.CheckExistedId(feature.AppFeatureId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, feature.AppFeatureId)
                });
            }

            var isExitedName = _featureService.CheckFeatureNameExisted(feature.FeatureName, feature.ParentFeatureId, feature.AppFeatureId);
            if (isExitedName)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, feature.FeatureName)
                });
            }

            if (feature.ParentFeatureId.HasValue)
            {
                var isExistedParentId = _featureService.CheckExistedId(feature.ParentFeatureId.Value);
                if (!isExistedParentId)
                {
                    return BadRequest(new BadRequestResponse()
                    {
                        Message = string.Format("Parent Id: " + CommonMessageList.NOT_FOUND_THE_ID, feature.ParentFeatureId.Value)
                    });
                }
            }

            AppFeatureView appfeature = _featureService.UpdateFeature(feature);
            return Ok(appfeature);
        }

        // DELETE api/<AppFeatureController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var canDelete = _featureService.CheckCanDelete(id);
            if (!canDelete)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = "Can not delete item which is included step which step source is code"
                });
            }
            _featureService.DeleteFeature(id);
            return Ok();
        }
    }
}
