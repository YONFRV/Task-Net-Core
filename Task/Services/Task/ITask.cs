using Task.Models;

namespace Task.Services.Task
{
    public interface ITask
    {
        ResponseModel<string> getByTask(string id);
    }
}
