using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.TypeState
{
    /// <summary>
    /// Clase que proporciona CRUD con los tipos de estado.
    /// </summary>
    public class TypeStateService : ITypeStateService
    {
        private readonly ILogger<TypeStateService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private TypeStateEntity? typeState;
        private IResponse response = new Response();
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;


        public TypeStateService(ILogger<TypeStateService> logger, SeeriContext seeriContext)
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
        public ResponseGeneralModel<string> FullTypeState()
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
        /// <summary>
        /// Este método trae un tipo de estado teniendo en cuenta el id asociado para la consulta.
        /// </summary>
        /// <param name="idTypeState">Es el Id de tipo de dato que se quiere consultar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        public ResponseGeneralModel<string> ByTypeState(int idTypeState)
        {
            TypeStateEntity? TypeState = GetTypeState(idTypeState);
            if (TypeState != null)
            {
                responseProcess = response.ResponseTrue("Dato encontrado", stateCodeTrue, JsonSerializer.Serialize(TypeState));
            }
            else
            {
                responseProcess = response.ResponseTrue("Dato no encontrado con el id: " + idTypeState, stateCodeFail, "");
            }
            return responseProcess;
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
            responseProcess = ProcessSaveAndUpdateDb(typeState, 1);
            return responseProcess;
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
            int typeStateId = (int)requestDataTypeState.TypeStateId;
            typeState = GetTypeState(typeStateId);
            if (typeState != null)
            {
                typeState.Name = requestDataTypeState.Name;
                typeState.UpdateDate = DateTime.Now;
                responseProcess = ProcessSaveAndUpdateDb(typeState, 2);
            }
            else
            {
                responseProcess = response.ResponseFalse("Error, el tipo de dato no esta creado" + typeStateId, 404, JsonSerializer.Serialize(requestDataTypeState));
            }
            return responseProcess;
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
        /// <summary>
        /// Este método Realiza el proceso de crear o actualizar el dato en la tabla type_data.
        /// </summary>
        /// <param name="typeState">es el modelo de Tipo de dato a crear o actualizar</param>
        /// <param name="typProcess">es el valor de entero qeu selecciona el proceso a ejecutar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private ResponseGeneralModel<string> ProcessSaveAndUpdateDb(TypeStateEntity typeState, int typProcess)
        {
            ResponseGeneralModel<string> responseProcessDb = new ResponseGeneralModel<string>();
            try
            {
                if (typProcess == 1)
                {
                    _context.Add(typeState);
                }
                else if (typProcess == 2)
                {
                    _context.Update(typeState);
                }
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

        private TypeStateEntity GetTypeState(int idTypeState)
        {
            TypeStateEntity typeState = new TypeStateEntity();
            try
            {
                typeState = _context.TypeStates.FirstOrDefault(x => x.TypeStateId == idTypeState);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error consultando TypeState Id: " + idTypeState + ex);
            }
            return typeState;
        }


    }
}
