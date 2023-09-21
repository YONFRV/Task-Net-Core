using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.TypeStateService.UpdateTypeStateService
{
    public class PutTypeStateService : IPutTypeStateService
    {
        private readonly ILogger<PutTypeStateService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private TypeStateEntity? typeState;
        private IResponse response = new Response();
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;

        public PutTypeStateService(ILogger<PutTypeStateService> logger, SeeriContext seeriContext)
        {
            _context = seeriContext;
            _logger = logger;
            responseProcess = new ResponseGeneralModel<string>();
        }

        /// <summary>
        /// Este método Realiza la consulta del dato que se quiere actualizar.
        /// </summary>
        /// <param name="RequestTypeStateModel">Es el modelo  del request solicitado para actualizar el tipo de dato</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        public ResponseGeneralModel<string> UpdateTypeState(RequestTypeStateModel requestDataTypeState)
        {
            typeState = new TypeStateEntity();
            try
            {
                typeState = _context.TypeStates.FirstOrDefault(x => x.TypeStateId == (int)requestDataTypeState.TypeStateId);
                if (typeState != null)
                {
                    typeState.Name = requestDataTypeState.Name;
                    typeState.UpdateDate = DateTime.Now;
                    responseProcess = ProcessUpdateDb(typeState);
                }
                else
                {
                    responseProcess = response.ResponseFalse("Error, el tipo de dato no esta creado", 404, JsonSerializer.Serialize(requestDataTypeState));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error consultando TypeState Id: " + (int)requestDataTypeState.TypeStateId + ex);
            }
            return responseProcess;
        }
        /// <summary>
        /// Este método Realiza el proceso de  actualizar el dato en la tabla type_data.
        /// </summary>
        /// <param name="typeState">es el modelo de Tipo de dato  actualizar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private ResponseGeneralModel<string> ProcessUpdateDb(TypeStateEntity typeState)
        {
            ResponseGeneralModel<string> responseProcessDb = new ResponseGeneralModel<string>();
            try
            {
                _context.Update(typeState);
                _context.SaveChanges();
                responseProcessDb = response.ResponseTrue("Proceso ejecutado", stateCodeTrue, JsonSerializer.Serialize(typeState));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución del proceso de db: " + ex);
                responseProcessDb = response.ResponseFalse("Error en ejecución del proceso de db: " + ex, stateCodeFail, JsonSerializer.Serialize(typeState));
            }
            return responseProcessDb;
        }
    }
}
