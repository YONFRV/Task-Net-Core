using Task.Models;

namespace Task.Services.Task.GetTaskService
{
    public interface IGetAllTaskService
    {
        ResponseGeneralModel<string> GetAllTask();
    }
}
