using Task.Models;

namespace Task.Services.Task.DeleteTaskService
{
    public interface IDeleteTaskService
    {
        ResponseGeneralModel<string> DeleteTask(int idTask);
    }
}
