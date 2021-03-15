using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.DataBaseModel.Commands;
using QPCore.Model.DataBaseModel.Configurations;
using QPCore.Model.TestRun;
using QPCore.Service;
using QPTestClient.QPFlow.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AnyOrignPolicy")]
    [ApiController]
    public class TestRunController : ControllerBase
    {
        private TestRunService testRunService;
        public TestRunController(TestRunService pcommandService)
        {
            testRunService = pcommandService;
        }
       
        [HttpPost]
        [Route("Batch")]
        public int PostBatch(Batch value)
        {
           return testRunService.Createbatch(value);
        }
        [HttpPut]
        [Route("Batch/{id}")]
        public int PutBatch(int id,Batch value)
        {
            return testRunService.Updatebatch(value,id);
        }

        // POST api/<CommandController>
        [HttpPost]
        [Route("Run")]
        public int PostRun(Run value)
        {
            return testRunService.Createrun(value);
        }
        [HttpPut]
        [Route("Run/{id}")]
        public int PutRun(int id,Run value)
        {
            return testRunService.Updaterun(value,id);
        }

        [HttpPost]
        [Route("TestCase")]
        public int PostTestCase(TestCase value)
        {
            return testRunService.Createtestcase(value);
        }
        [HttpPut]
        [Route("TestCase/{id}")]
        public int PutTestCase(int id, TestCase value)
        {
            return testRunService.Updatetest(value, id);
        }
        [HttpPost]
        [Route("TestStep")]
        public int PostTestStep(TestStep value)
        {
            return testRunService.Createteststep(value);
        }
        [HttpPost]
        [Route("TestResult")]
        public int PostTestResult(TestRunResult value)
        {
            return testRunService.Addresults(value);
        }
        [HttpGet()]
        [Route("TestResult/{id}")]
        public TestRunResult GetResults(int id)
        {
            return testRunService.getTestResult(id);
        }
    }
}
