using Task.Models;

namespace Task.Services.Task.PostTaskService
{
    public interface IPostTaskService
    {
        ResponseGeneralModel<string> SaveTask(RequestTask dataTask);
    }
}
