using Task.Models;

namespace Task.Services.TypeStateService.UpdateTypeStateService
{
    public interface IPutTypeStateService
    {
        ResponseGeneralModel<string> UpdateTypeState(RequestTypeStateModel requestDataTypeState);
    }
}
