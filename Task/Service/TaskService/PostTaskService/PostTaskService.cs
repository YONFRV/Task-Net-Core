using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.Task.PostTaskService
{
    public class PostTaskService : IPostTaskService
    {
        private readonly ILogger<PostTaskService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private IResponse response;
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;
        public PostTaskService(ILogger<PostTaskService> logger, SeeriContext seeriContext)
        {
            _logger = logger;
            _context = seeriContext;
            response = new Response();
            responseProcess = new ResponseGeneralModel<string>();
        }
        /// <summary>
        /// Este método guarda la tarea.
        /// </summary>
        /// <param name="dataTask">Es el modelo  del request solicitado para crear la tarea</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        public ResponseGeneralModel<string> SaveTask(RequestTask dataTask)
        {
            TaskEntity task = new TaskEntity();
            task.Titulo = dataTask.Titulo;
            task.Descripcion = dataTask.Descripcion;
            task.State = dataTask.State;
            task.CreateDate = DateTime.Now;
            responseProcess = ValidateSaveDataTask(task);
            return responseProcess;
        }
        /// <summary>
        /// Este método valida si el tipo de dato existe.
        /// </summary>
        /// <param name="task">modelo de tarea</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        private ResponseGeneralModel<string> ValidateSaveDataTask(TaskEntity task)
        {
            try
            {
                TypeStateEntity typeState = _context.TypeStates.FirstOrDefault(x => x.TypeStateId == task.State);
                if (typeState != null)
                {
                    responseProcess = ProcessSaveTask(task);
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
        /// crea la tarea.
        /// </summary>
        /// <param name="task">es la tarea que sae va a crear</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private ResponseGeneralModel<string> ProcessSaveTask(TaskEntity task)
        {
            try
            {
                _context.Add(task);
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
