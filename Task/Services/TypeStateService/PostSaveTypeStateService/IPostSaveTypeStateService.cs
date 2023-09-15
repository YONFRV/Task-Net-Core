using Task.Models;

namespace Task.Services.TypeStateService.PostSaveTypeStateService
{
    public interface IPostSaveTypeStateService
    {
        ResponseGeneralModel<string> SaveTypeState(RequestTypeStateModel requestDataTypeState);
    }
}
