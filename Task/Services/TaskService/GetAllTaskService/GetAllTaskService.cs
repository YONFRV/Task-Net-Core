using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.Task.GetTaskService
{
    public class GetAllTaskService : IGetAllTaskService
    {
        private readonly ILogger<GetAllTaskService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private IResponse response;
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;
        public GetAllTaskService(ILogger<GetAllTaskService> logger, SeeriContext seeriContext)
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
        public ResponseGeneralModel<string> GetAllTask()
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
    }
}
