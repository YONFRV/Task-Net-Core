using Task.Models;

namespace Task.Services.Task
{
    public class TaskEj : ITask
    {
        private readonly ILogger<TaskEj> _logger;
        private  ResponseModel<string> response = new ResponseModel<string>();

        public TaskEj(ILogger<TaskEj> logger) {
            _logger = logger;
        }

        public ResponseModel<string> getByTask(string id) {
            _logger.LogInformation("Desde el task: "+ id);
            return response;
        }
    }
}
