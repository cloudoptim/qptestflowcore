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
        public Steps Post(Steps value)
        {
            return _stepService.CreateStep(value);
        }

        // PUT api/<StepController>/5
        [HttpPut("{id}")]
        public Steps Put(int id, Steps value)
        {
            value.StepId = id;
            return _stepService.UpdateStep(value);
        }

        // DELETE api/<StepController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isUsed = _stepService.CheckIsUsed(id);
            if (isUsed)
            {
                return BadRequest("This step is being used by a TestFlow. Please remove it in the TestFlow before you do this action.");
            }
            else
            {
                _stepService.DeleteStep(id);
                return Ok();
            }
        }

        [HttpGet("checkunique")]
        public CheckUniqueDTO CheckUniqueStepGlossary(int featureId, string stepName)
        {
            var result = _stepService.CheckUniqueStepGlossary(featureId, stepName);
            return result;
        }
    }
}
