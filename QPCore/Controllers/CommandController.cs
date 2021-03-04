using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.DataBaseModel.Commands;
using QPCore.Service;
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
    public class CommandController : ControllerBase
    {
        private CommandService commandService;
        public CommandController(CommandService pcommandService)
        {
            commandService = pcommandService;
        }
        // GET: api/<CommandController>
        [HttpGet]
        public List<Command> Get()
        {
            return commandService.getCommands();
        }

        // GET api/<CommandController>/5
        [HttpGet("{id}")]
        public Command Get(int id)
        {
            return commandService.getCommand(id);
        }

        // POST api/<CommandController>
        [HttpPost]
        public Command Post(Command value)
        {
           return  commandService.CreateCommand(value);
        }

        // PUT api/<CommandController>/5
        [HttpPut("{id}")]
        public Command Put(int id, Command value)
        {
            return commandService.UpdateCommand(id,value);
        }

        // DELETE api/<CommandController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            commandService.DeleteCommand(id);
        }
    }
}
