using Task.Data.Entities;
using Task.Data;
using Task.Models;
using Task.Services.ResponseProceso;
using System.Text.Json;

namespace Task.Services.TypeStateService.GetAllTypeStateService
{
    public class GetAllTypeStateService : IGetAllTypeStateService
    {
        private readonly ILogger<GetAllTypeStateService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private TypeStateEntity? typeState;
        private IResponse response = new Response();
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;


        public GetAllTypeStateService(ILogger<GetAllTypeStateService> logger, SeeriContext seeriContext)
        {
            _context = seeriContext;
            _logger = logger;
            responseProcess = new ResponseGeneralModel<string>();
        }
        /// <summary>
        /// Este método trae todos los tipos de estado.
        /// </summary>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        public ResponseGeneralModel<string> GetAllTypeState()
        {
            try
            {
                List<TypeStateEntity> listTypeState = _context.TypeStates.ToList();
                if (listTypeState.Count > 0)
                {
                    responseProcess = response.ResponseTrue("Datos encontrados: " + listTypeState.Count, stateCodeTrue, JsonSerializer.Serialize(listTypeState));

                }
                else
                {
                    responseProcess = response.ResponseTrue("sin datos", stateCodeFail, "");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de consulta de db " + ex);
                responseProcess = response.ResponseFalse("Error en ejecución de consulta de db " + ex, stateCodeFail, "");
            }
            return responseProcess;
        }
    }
}
