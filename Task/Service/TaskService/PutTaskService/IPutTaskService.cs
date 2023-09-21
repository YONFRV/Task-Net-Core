using Task.Models;

namespace Task.Services.Task.PutTaskService
{
    public interface IPutTaskService
    {
        ResponseGeneralModel<string> UpdateTask(RequestTask dataTask);
    }
}
