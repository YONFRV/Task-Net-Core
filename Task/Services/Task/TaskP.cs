using Task.Models;

namespace Task.Services.Task
{

    public class TaskP
    {
        private readonly ITask _itask;

        public TaskP(ITask task) {
            _itask = task;
        }

        public ResponseModel<string> getByTask(string id)
        {   
            return _itask.getByTask(id);
        }
    }
}
