using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.Task.PutTaskService
{
    public class PutTaskService : IPutTaskService
    {
        private readonly ILogger<PutTaskService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private IResponse response;
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;
        public PutTaskService(ILogger<PutTaskService> logger, SeeriContext seeriContext)
        {
            _logger = logger;
            _context = seeriContext;
            response = new Response();
            responseProcess = new ResponseGeneralModel<string>();
        }

        /// <summary>
        /// Este método valida la tarea a actualziar-.
        /// </summary>
        /// <param name="dataTask">Es el modelo  del request solicitado para crear la tarea</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        public ResponseGeneralModel<string> UpdateTask(RequestTask dataTask)
        {
            try
            {
                TaskEntity resultTask = _context.Tasks.FirstOrDefault(x => x.TaskId == (int)dataTask.TaskId);
                if (resultTask != null)
                {
                    resultTask.Titulo = dataTask.Titulo;
                    resultTask.Descripcion = dataTask.Descripcion;
                    resultTask.State = dataTask.State;
                    resultTask.UpdateDate = DateTime.Now;
                    responseProcess = ValidateUpdateDataTask(resultTask);
                }
                else
                {
                    responseProcess = response.ResponseFalse("Error, la tarea no esta creado", stateCodeFail, JsonSerializer.Serialize(dataTask));
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
        /// Este método valida si el tipo de dato existe.
        /// </summary>
        /// <param name="task">modelo de tarea</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        private ResponseGeneralModel<string> ValidateUpdateDataTask(TaskEntity task)
        {
            try
            {
                TypeStateEntity typeState = _context.TypeStates.FirstOrDefault(x => x.TypeStateId == task.State);
                if (typeState != null)
                {
                    responseProcess = ProcessUpdateTask(task);
                }
                else
                {
                    responseProcess = response.ResponseFalse("Error el tipo de dato no esta creado", stateCodeFail, JsonSerializer.Serialize(task));
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
        /// Actualiza o crea la tarea.
        /// </summary>
        /// <param name="task">es la tarea que sae va a crear</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private ResponseGeneralModel<string> ProcessUpdateTask(TaskEntity task)
        {
            try
            {
                _context.Update(task);
                _context.SaveChanges();
                responseProcess = response.ResponseTrue("Proceso ejecutado", stateCodeTrue, JsonSerializer.Serialize(task));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución del proceso de db: " + ex);
                responseProcess = response.ResponseFalse("Error en ejecución del proceso de db: " + ex, stateCodeFail, JsonSerializer.Serialize(task));
            }
            return responseProcess;
        }
    }
}
