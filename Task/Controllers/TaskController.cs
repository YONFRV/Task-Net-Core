using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Services.Task.DeleteTaskService;
using Task.Services.Task.GetByTaskService;
using Task.Services.Task.GetTaskService;
using Task.Services.Task.PostTaskService;
using Task.Services.Task.PutTaskService;

namespace Task.Controllers
{
    [Route("api/v1")]
    public class TaskController : Controller
    {

        private readonly IGetAllTaskService _iGetAllTaskService;
        private readonly IGetByTaskService _iGetByTaskService;
        private readonly IPostTaskService _iPostTaskService;
        private readonly IPutTaskService _iPutTaskService;
        private readonly IDeleteTaskService _iDeleteTaskService;

        public TaskController(
            IGetAllTaskService getAllTaskService,
            IGetByTaskService getByTaskService,
            IPostTaskService postTaskService,
            IPutTaskService putTaskService,
            IDeleteTaskService deleteTaskService
        )
        {
            _iGetAllTaskService = getAllTaskService;
            _iGetByTaskService = getByTaskService;
            _iPostTaskService = postTaskService;
            _iPutTaskService = putTaskService;
            _iDeleteTaskService = deleteTaskService;
        }

        [HttpGet]
        [Route("task")]
        public IActionResult GetAllTask()
        {
            ResponseGeneralModel<string> respons = _iGetAllTaskService.GetAllTask();
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpGet]
        [Route("by-task/{idTask}")]
        public IActionResult GetByTask([FromRoute] int idTask)
        {
            ResponseGeneralModel<string> respons = _iGetByTaskService.GetByTask(idTask);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpPost]
        [Route("save-task")]
        public IActionResult SaveTask([FromBody] RequestTask dataTask)
        {
            ResponseGeneralModel<string> respons = _iPostTaskService.SaveTask(dataTask);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpPut]
        [Route("update-task/{idTask}")]
        public IActionResult UpdateTask([FromBody] RequestTask dataTask, [FromRoute] int idTask)
        {
            dataTask.TaskId = idTask;
            ResponseGeneralModel<string> respons = _iPutTaskService.UpdateTask(dataTask);
            return StatusCode(respons.Status, new { respons.response });

        }

        [HttpDelete]
        [Route("delete-task/{idTask}")]
        public IActionResult DeleteTask([FromRoute] int idTask)
        {
            ResponseGeneralModel<string> respons = _iDeleteTaskService.DeleteTask(idTask);
            return StatusCode(respons.Status, new { respons.response });
        }
    }
}
