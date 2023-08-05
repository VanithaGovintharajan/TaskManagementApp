using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Models;
using TaskManagement.Services.Contracts;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly IUserTaskService _userTaskService;

        public UserTaskController(IUserTaskService _userTaskService)
        {
            this._userTaskService = _userTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserTasks()
        {
            var userTask = _userTaskService.GetAllUserTasks();
            return Ok(userTask);
        }

        [HttpGet("{userTaskId}")]
        public async Task<IActionResult> GetUserTaskById(Guid userTaskId)
        {
            var userTask = _userTaskService.GetUserTaskById(userTaskId);
            if (userTask == null)
                return NotFound();

            return Ok(userTask);
        }
                      
        [HttpPost]
        public async Task<IActionResult> CreateUserTask([FromBody] UserTask userTask)
        {
            if (userTask == null)
            {
                return BadRequest();
            }

            var createdTask = _userTaskService.CreateUserTask(userTask);
            return Created("Task Created", createdTask);            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserTask(UserTask userTask)
        {
            if (userTask == null)
            {
                return BadRequest();
            }

            var updatedTask = _userTaskService.UpdateUserTask(userTask);
            return Ok(updatedTask);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserTask(Guid userTaskId)
        {
            if (userTaskId == Guid.Empty)
            {
                return BadRequest();
            }

            _userTaskService.DeleteUserTask(userTaskId);
            return Ok("Task deleted");
        }

    }
}
