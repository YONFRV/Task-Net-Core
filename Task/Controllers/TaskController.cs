using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Services.Task;

namespace Task.Controllers
{
    [Route("api/v1")]
    public class TaskController : Controller
    {

        private readonly ITaskService _itask;

        public TaskController( ITaskService itask)
        {
            _itask = itask;
        }

        [HttpGet]
        [Route("task")]
        public IActionResult Task()
        {
            ResponseGeneralModel<string> respons = _itask.FullTask();
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpGet]
        [Route("by-task/{idTask}")]
        public IActionResult ByTask([FromRoute] int idTask)
        {
            ResponseGeneralModel<string> respons = _itask.ByTask(idTask);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpPost]
        [Route("save-task")]
        public IActionResult SaveTask([FromBody] RequestTask dataTask)
        {
            ResponseGeneralModel<string> respons = _itask.SaveTask(dataTask);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpPut]
        [Route("update-task/{idTask}")]
        public IActionResult UpdateTask([FromBody] RequestTask dataTask, [FromRoute] int idTask)
        {
            dataTask.TaskId = idTask;
            ResponseGeneralModel<string> respons = _itask.UpdateTask(dataTask);
            return StatusCode(respons.Status, new { respons.response });

        }

        [HttpDelete]
        [Route("delete-task/{idTask}")]
        public IActionResult DeleteTask([FromRoute] int idTask)
        {
            ResponseGeneralModel<string> respons = _itask.DeleteTask(idTask);
            return StatusCode(respons.Status, new { respons.response });
        }
    }
}
