using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Services;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Backend.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class DBtasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public DBtasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DBtask>>> Gettasks(int? status)
        {
            var results = await _taskService.GetTasks(status);
            return Ok(results);
        }
        

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDBtask(int id, DBtask dBtask)
        {
            
            var Result = await _taskService.UpdateTask(id, dBtask);
            return NoContent();
        }

        // POST: api/tasks
        [ActionName("GetDBtask")]
        [HttpPost]
        public async Task<ActionResult<DBtask>> PostDBtask(DBtask dBtask)
        {
            if (_taskService.taskExistsByName(dBtask.TaskName))
            {
                return BadRequest();
            }
            var task = await _taskService.PostTask(dBtask);
            return CreatedAtAction("GetDBtask", new { id = dBtask.Id }, task);
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DBtask>> DeleteDBtask(int id)
        {
            var dBtask = await _taskService.GetTask(id);

            if (dBtask == null)
            {
                return NotFound();
            }

            return await _taskService.DeleteTask(id);

        }

        
        
    }
}