using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.TypeStateService.DeleteTypeStateService
{
    public class DeleteTypeStateService: IDeleteTypeStateService
    {
        private readonly ILogger<DeleteTypeStateService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private TypeStateEntity? typeState;
        private IResponse response = new Response();
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;

        public DeleteTypeStateService(ILogger<DeleteTypeStateService> logger, SeeriContext seeriContext)
        {
            _context = seeriContext;
            _logger = logger;
            responseProcess = new ResponseGeneralModel<string>();
        }
        /// <summary>
        /// Este método elimina un tipo de estado.
        /// </summary>
        /// <param name="idTypeState">Es el Id de tipo de dato que se quiere consultar para ser eliminado</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        public ResponseGeneralModel<string> DeleteTypeState(int idTypeState)
        {
            typeState = new TypeStateEntity();
            try
            {
                typeState = _context.TypeStates.FirstOrDefault(x => x.TypeStateId == idTypeState);
                if (typeState != null)
                {
                    responseProcess = ValidateDeleteTypeStat(typeState);
                }
                else
                {
                    responseProcess = response.ResponseTrue("dato no encontrado con el id: " + idTypeState, stateCodeFail, "");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de conculsta de db " + ex);
                responseProcess = response.ResponseFalse("Error en ejecución del proceso de db: " + ex, stateCodeFail, "");
            }
            return responseProcess;
        }
        /// <summary>
        /// Valida si el dato esta siendo usado en la tabla Task.
        /// </summary>
        /// <param name="typeState">Es el modelo  del request solicitado para actualizar el tipo de dato</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private ResponseGeneralModel<string> ValidateDeleteTypeStat(TypeStateEntity typeState)
        {
            try
            {
                List<TaskEntity> task = _context.Tasks.Where(x => x.State == typeState.TypeStateId).ToList();
                if (task.Count > 0)
                {
                    responseProcess = response.ResponseTrue("El tipo de dato está siendo usado en task, no se puede eliminar", stateCodeFail, JsonSerializer.Serialize(typeState));
                }
                else
                {
                    responseProcess = ProcessDeleteTypeStat(typeState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de conculsta de db " + ex);
                responseProcess = response.ResponseFalse("Error en ejecución del proceso de db: " + ex, stateCodeFail, "");
            }
            return responseProcess;
        }
        /// <summary>
        /// Este método Realiza eliminacion del tipo de dato d el atabla type_data.
        /// </summary>
        /// <param name="typeState">es el modelo de Tipo de dato a aliminar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private ResponseGeneralModel<string> ProcessDeleteTypeStat(TypeStateEntity typeState)
        {
            try
            {
                _context.TypeStates.Remove(typeState);
                _context.SaveChanges();
                responseProcess = response.ResponseTrue("Dato eliminado", stateCodeTrue, JsonSerializer.Serialize(typeState));

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de eliminar dato en db " + ex);
                responseProcess = response.ResponseTrue("Error en ejecución de eliminar dato en db " + ex, stateCodeFail, "");
            }
            return responseProcess;

        }
    }
}
