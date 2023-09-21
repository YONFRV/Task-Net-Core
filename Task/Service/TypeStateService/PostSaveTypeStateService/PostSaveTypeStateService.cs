using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.TypeStateService.PostSaveTypeStateService
{
    public class PostSaveTypeStateService : IPostSaveTypeStateService
    {
        private readonly ILogger<PostSaveTypeStateService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private TypeStateEntity? typeState;
        private IResponse response = new Response();
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;

        public PostSaveTypeStateService(ILogger<PostSaveTypeStateService> logger, SeeriContext seeriContext)
        {
            _context = seeriContext;
            _logger = logger;
            responseProcess = new ResponseGeneralModel<string>();
        }
        /// <summary>
        /// Este método guarda el tipo de dato.
        /// </summary>
        /// <param name="RequestTypeStateModel">Es el modelo  del request solicitado para crear el tipo de dato</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        public ResponseGeneralModel<string> SaveTypeState(RequestTypeStateModel requestDataTypeState)
        {
            typeState = new TypeStateEntity();
            typeState.Name = requestDataTypeState.Name;
            typeState.CreateDate = DateTime.Now;
            responseProcess = ProcessSave(typeState, 1);
            return responseProcess;
        }

        /// <summary>
        /// Este método Realiza el proceso de crear o actualizar el dato en la tabla type_data.
        /// </summary>
        /// <param name="typeState">es el modelo de Tipo de dato a crear o actualizar</param>
        /// <param name="typProcess">es el valor de entero qeu selecciona el proceso a ejecutar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private ResponseGeneralModel<string> ProcessSave(TypeStateEntity typeState, int typProcess)
        {
            try
            {
                _context.Add(typeState);
                _context.SaveChanges();
                responseProcess = response.ResponseTrue("Proceso ejecutado", stateCodeTrue, JsonSerializer.Serialize(typeState));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución del proceso de db: " + ex);
                responseProcess = response.ResponseFalse("Error en ejecución del proceso de db: " + ex, stateCodeFail, JsonSerializer.Serialize(typeState));
            }
            return responseProcess;
        }
    }
}
