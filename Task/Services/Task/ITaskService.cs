using Task.Models;

namespace Task.Services.Task
{
    public interface ITaskService
    {
        ResponseGeneralModel<string> FullTask();
        ResponseGeneralModel<string> ByTask(int idTask);
        ResponseGeneralModel<string> SaveTask(RequestTask dataTask);
        ResponseGeneralModel<string> UpdateTask(RequestTask dataTask);
        ResponseGeneralModel<string> DeleteTask(int idTask);
    }
}
