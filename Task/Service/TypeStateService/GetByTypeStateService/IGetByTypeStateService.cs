using Task.Models;

namespace Task.Services.TypeStateService.GetByTypeStateService
{
    public interface IGetByTypeStateService
    {
        ResponseGeneralModel<string> GetByTypeState(int idTypeState);
    }
}
