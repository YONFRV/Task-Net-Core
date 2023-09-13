using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private IResponse response;
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;
        public TaskService(ILogger<TaskService> logger, SeeriContext seeriContext)
        {
            _logger = logger;
            _context = seeriContext;
            response = new Response();
            responseProcess = new ResponseGeneralModel<string>();
        }


        /// <summary>
        /// Este método trae todas las tareas.
        /// </summary>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        public ResponseGeneralModel<string> FullTask()
        {
            try
            {
                List<TaskEntity> listTask = _context.Tasks.ToList();
                if (listTask.Count > 0)
                {
                    responseProcess = response.ResponseTrue("Datos encontrados: " + listTask.Count, stateCodeTrue, JsonSerializer.Serialize(listTask));

                }
                else
                {
                    responseProcess = response.ResponseTrue("sin datos", stateCodeTrue, "");
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
        /// Este método trae un tarea teniendo en cuenta el id asociado para la consulta.
        /// </summary>
        /// <param name="idTask">Es el Id de la tarea que se quiere consultar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        public ResponseGeneralModel<string> ByTask(int idTask)
        {
            TaskEntity listTask = GetTask(idTask);
            if (listTask != null)
            {
                responseProcess = response.ResponseTrue("Dato encontrado", stateCodeTrue, JsonSerializer.Serialize(listTask));

            }
            else
            {
                responseProcess = response.ResponseTrue("Dato no encontrado", stateCodeTrue, "");
            }
            return responseProcess;
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
            responseProcess = ValidateSaveDataTask(task, 1);
            return responseProcess;
        }
        /// <summary>
        /// Este método valida la tarea a actualziar-.
        /// </summary>
        /// <param name="dataTask">Es el modelo  del request solicitado para crear la tarea</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        public ResponseGeneralModel<string> UpdateTask(RequestTask dataTask)
        {
            int idTask = (int)dataTask.TaskId;
            TaskEntity dataTaskProcess = GetTask(idTask);
            if (dataTaskProcess != null)
            {
                dataTaskProcess.Titulo = dataTask.Titulo;
                dataTaskProcess.Descripcion = dataTask.Descripcion;
                dataTaskProcess.State = dataTask.State;
                dataTaskProcess.UpdateDate = DateTime.Now;
                responseProcess = ValidateSaveDataTask(dataTaskProcess, 2);
            }
            else
            {
                responseProcess = response.ResponseFalse("Error, la tarea no esta creado", stateCodeFail, JsonSerializer.Serialize(dataTask));
            }
            return responseProcess;
        }

        public ResponseGeneralModel<string> DeleteTask(int idTask)
        {
            TaskEntity dataTaskProcess = GetTask(idTask);
            if (dataTaskProcess != null)
            {
                responseProcess = ProcessDeleteTask(dataTaskProcess);
            }
            else
            {
                responseProcess = response.ResponseFalse("Error, la tarea no esta creado con id= " + idTask, stateCodeFail, "");
            }
            return responseProcess;
        }

        private ResponseGeneralModel<string> ProcessDeleteTask(TaskEntity dataTaskProcess)
        {
            try
            {
                _context.Tasks.Remove(dataTaskProcess);
                _context.SaveChanges();
                responseProcess = response.ResponseTrue("Dato eliminado", stateCodeTrue, JsonSerializer.Serialize(dataTaskProcess));

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de eliminar dato en db " + ex);
                responseProcess = response.ResponseTrue("Error en ejecución de eliminar dato en db " + ex, stateCodeFail, "");
            }
            return responseProcess;
        }


        /// <summary>
        /// Este método valida si el tipo de dato existe.
        /// </summary>
        /// <param name="task">modelo de tarea</param>
        /// <param name="process">Define el proceso a ejecutar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        private ResponseGeneralModel<string> ValidateSaveDataTask(TaskEntity task, int process)
        {
            TypeStateEntity typeState = GetTypeState(task.State);
            if (typeState != null)
            {
                responseProcess = ProcessSaveOrUpdateTask(task, process);
            }
            else
            {
                responseProcess = response.ResponseFalse("Error el tipo de dato no esta creado", stateCodeFail, JsonSerializer.Serialize(task));
            }
            return responseProcess;
        }
        /// <summary>
        /// Actualiza o crea la tarea.
        /// </summary>
        /// <param name="task">es la tarea que sae va a crear</param>
        /// <param name="process">es el proceso que va a ejecutar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private ResponseGeneralModel<string> ProcessSaveOrUpdateTask(TaskEntity task, int process)
        {
            try
            {
                if (process == 1)
                {
                    _context.Add(task);
                }
                else if (process == 2)
                {
                    _context.Update(task);
                }
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
        /// <summary>
        /// Consulta la tarea.
        /// </summary>
        /// <param name="idTask">es el id de la tarea a consultar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private TaskEntity GetTask(int idTask)
        {
            TaskEntity resultTask = new TaskEntity();
            try
            {
                resultTask = _context.Tasks.FirstOrDefault(x => x.TaskId == idTask);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de consulta de db " + ex);
            }
            return resultTask;
        }
        /// <summary>
        /// Consulta el tipo de estado.
        /// </summary>
        /// <param name="idTypeState">es el id del tipo de estado</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        /// <exception cref="Exception">Cuando la consulta no se puede realizar a la base de datos</exception>
        private TypeStateEntity GetTypeState(int? idTypeState)
        {
            TypeStateEntity typeState = new TypeStateEntity();
            try
            {
                typeState = _context.TypeStates.FirstOrDefault(x => x.TypeStateId == idTypeState);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de consulta de db " + ex);
                responseProcess = response.ResponseFalse("Error en ejecución de consulta de db " + ex, stateCodeFail, "");
            }
            return typeState;
        }
    }
}
