using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.TypeStateService.GetByTypeStateService
{
    public class GetByTypeStateService : IGetByTypeStateService
    {
        private readonly ILogger<GetByTypeStateService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private IResponse response = new Response();
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;


        public GetByTypeStateService(ILogger<GetByTypeStateService> logger, SeeriContext seeriContext)
        {
            _context = seeriContext;
            _logger = logger;
            responseProcess = new ResponseGeneralModel<string>();
        }
        /// <summary>
        /// Este método trae un tipo de estado teniendo en cuenta el id asociado para la consulta.
        /// </summary>
        /// <param name="idTypeState">Es el Id de tipo de dato que se quiere consultar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        public ResponseGeneralModel<string>GetByTypeState(int idTypeState)
        {
            try
            {
                TypeStateEntity typeState = _context.TypeStates.FirstOrDefault(x => x.TypeStateId == idTypeState);
                if (typeState != null)
                {
                    responseProcess = response.ResponseTrue("Dato encontrado", stateCodeTrue, JsonSerializer.Serialize(typeState));
                }
                else
                {
                    responseProcess = response.ResponseTrue("Dato no encontrado con el id: " + idTypeState, stateCodeFail, "");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error consultando TypeState Id: " + idTypeState + ex);
            }
            return responseProcess;
        }
    }
}
