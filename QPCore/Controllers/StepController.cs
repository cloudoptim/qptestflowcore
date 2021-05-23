using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.DataBaseModel;
using QPCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("AnyOrignPolicy")]
    [ApiController]
    public class StepController : ControllerBase
    {
        StepService _stepService;

        public StepController(StepService stepService)
        {
            _stepService = stepService;
        }

        // GET api/<StepController>/5
        [HttpGet("{id}")]
        public Steps Get(int id)
        {
            return _stepService.Getstep(id);
        }

        // POST api/<StepController>
        [HttpPost]
        public ActionResult<Steps> Post(Steps value)
        {
            var isExisted = _stepService.CheckUniqueStepInFeature(value.FeatureId, value.StepName);
            if (!isExisted.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, value.StepName)
                });
            }
            var result = _stepService.CreateStep(value);
            return Ok(result);
        }

        // PUT api/<StepController>/5
        [HttpPut("{id}")]
        public ActionResult<Steps> Put(int id, Steps value)
        {
            value.StepId = id;

            var isExisted = _stepService.CheckUniqueStepInFeature(value.FeatureId, value.StepName, value.StepId);
            if (!isExisted.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, value.StepName)
                });
            }

            var result = _stepService.UpdateStep(value);
            return Ok(result);
        }

        // DELETE api/<StepController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isUsed = _stepService.CheckIsUsed(id);
            var canStepSourceCode = _stepService.CheckCanDeleteStepGlossary(id);
            if (isUsed)
            {
                return BadRequest("This step is being used by a TestFlow. Please remove it in the TestFlow before you do this action.");
            }
            else if(!canStepSourceCode)
            {
                return BadRequest("You aren't allowed to delete step with source equal to \"code\".");
            }
            else
            {
                _stepService.DeleteStep(id);
                return Ok();
            }
        }

        [HttpGet("checkunique")]
        public CheckUniqueResponse CheckUniqueStepGlossary(int featureId, string stepName)
        {
            var result = _stepService.CheckUniqueStepGlossary(featureId, stepName);
            return result;
        }
    }
}
