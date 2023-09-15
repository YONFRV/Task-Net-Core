using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.ResponseProceso;

namespace Task.Services.Task.DeleteTaskService
{
    public class DeleteTaskService : IDeleteTaskService
    {
        private readonly ILogger<DeleteTaskService> _logger;
        private readonly SeeriContext _context;
        private ResponseGeneralModel<string> responseProcess;
        private IResponse response;
        private int stateCodeFail = 404;
        private int stateCodeTrue = 200;
        public DeleteTaskService(ILogger<DeleteTaskService> logger, SeeriContext seeriContext)
        {
            _logger = logger;
            _context = seeriContext;
            response = new Response();
            responseProcess = new ResponseGeneralModel<string>();
        }
        public ResponseGeneralModel<string> DeleteTask(int idTask)
        {
            try
            {
                TaskEntity resultTask = _context.Tasks.FirstOrDefault(x => x.TaskId == idTask);
                if (resultTask != null)
                {
                    responseProcess = ProcessDeleteTask(resultTask);
                }
                else
                {
                    responseProcess = response.ResponseFalse("Error, la tarea no esta creado con id= " + idTask, stateCodeFail, "");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en ejecución de consulta de db " + ex);
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
    }
}
