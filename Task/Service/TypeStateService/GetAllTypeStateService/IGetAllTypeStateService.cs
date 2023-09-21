using Task.Models;

namespace Task.Services.TypeStateService.GetAllTypeStateService
{
    public interface IGetAllTypeStateService
    {
        ResponseGeneralModel<string> GetAllTypeState();
    }
}
