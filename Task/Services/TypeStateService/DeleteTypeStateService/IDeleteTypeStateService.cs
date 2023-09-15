using Task.Models;

namespace Task.Services.TypeStateService.DeleteTypeStateService
{
    public interface IDeleteTypeStateService
    {
        ResponseGeneralModel<string> DeleteTypeState(int idTypeState);
    }
}
