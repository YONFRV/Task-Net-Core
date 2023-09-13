using System.Text.Json;
using Task.Data.Entities;
using Task.Models;

namespace Task.Services.ResponseProceso
{
    public class Response : IResponse
    {
        private ResponseGeneralModel<string>? response;
        private ResponseModel<string>? resultProcess;

        public ResponseGeneralModel<string> ResponseFalse(string menssage, int state, string data) {
            response= new ResponseGeneralModel<string>();
            resultProcess = new ResponseModel<string>();
            resultProcess.message = menssage;
            resultProcess.warning = true;
            resultProcess.success = false;
            resultProcess.Data = data;
            response.Status = state;
            response.response = resultProcess;

            return response;
        }
        public ResponseGeneralModel<string> ResponseTrue(string menssage, int state, string data)
        {
            response = new ResponseGeneralModel<string>();
            resultProcess = new ResponseModel<string>();
            resultProcess.message = menssage;
            resultProcess.warning = false;
            resultProcess.success = true;
            resultProcess.Data = data; 
            response.Status = state;
            response.response = resultProcess;

            return response;
        }
    }
}
