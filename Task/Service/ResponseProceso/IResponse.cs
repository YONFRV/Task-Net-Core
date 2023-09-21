using Task.Data.Entities;
using Task.Models;

namespace Task.Services.ResponseProceso
{
    public interface IResponse
    {
        ResponseGeneralModel<string> ResponseFalse(string menssage, int state, string dat);
        ResponseGeneralModel<string> ResponseTrue(string menssage, int state, string data);
    }
}
