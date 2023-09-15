using Task.Models;

namespace Task.Services.Task.GetByTaskService
{
    public interface IGetByTaskService
    {
        ResponseGeneralModel<string> GetByTask(int idTask);
    }
}
