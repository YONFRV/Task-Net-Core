using Task.Models;

namespace Task.Services.TypeState
{
    public interface ITypeStateService
    {
        ResponseGeneralModel<string> FullTypeState();
        ResponseGeneralModel<string> ByTypeState(int idTypeState);
        ResponseGeneralModel<string> SaveTypeState(RequestTypeStateModel RequestDataTypeState);
        ResponseGeneralModel<string> UpdateTypeState(RequestTypeStateModel RequestDataTypeState);
        ResponseGeneralModel<string> DeleteTypeState(int idTypeState);
    }
}
