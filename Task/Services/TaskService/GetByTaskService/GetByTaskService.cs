using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.Task.GetByTaskService
{
    public class GetByTaskService : IGetByTaskService
    {
        private readonly ILogger<GetByTaskService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private IResponse response;
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;
        public GetByTaskService(ILogger<GetByTaskService> logger, SeeriContext seeriContext)
        {
            _logger = logger;
            _context = seeriContext;
            response = new Response();
            responseProcess = new ResponseGeneralModel<string>();
        }

        /// <summary>
        /// Este método trae un tarea teniendo en cuenta el id asociado para la consulta.
        /// </summary>
        /// <param name="idTask">Es el Id de la tarea que se quiere consultar</param>
        /// <returns>Devuelve como respuesta un modelo de ResponseModel y una variable int que se toma como el estado HTTP</returns>
        public ResponseGeneralModel<string> GetByTask(int idTask)
        {
            TaskEntity resultTask = new TaskEntity();
            try
            {
                resultTask = _context.Tasks.FirstOrDefault(x => x.TaskId == idTask);
                if (resultTask != null)
                {
                    responseProcess = response.ResponseTrue("Dato encontrado", stateCodeTrue, JsonSerializer.Serialize(resultTask));

                }
                else
                {
                    responseProcess = response.ResponseTrue("Dato no encontrado", stateCodeTrue, "");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de consulta de db " + ex);
                responseProcess = response.ResponseFalse("Error en ejecución de consulta de db tabla task", stateCodeFail, "");

            }
            return responseProcess;
        }
    }
}
