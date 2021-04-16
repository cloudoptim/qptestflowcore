﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.DataBaseModel;
using QPCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AnyOrignPolicy")]
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
        public void Delete(int id)
        {
            _stepService.DeleteStep(id);
        }

        [HttpGet("checkunique")]
        public ActionResult CheckUniqueStepGlossary(int featureId, string stepName)
        {
            var result = _stepService.CheckUniqueStepGlossary(featureId, stepName);
            return Ok(new { isUnique = result });
        }
    }
}
