using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Services.Task;

namespace Task.Controllers
{
    [Route("api/v1")]
    public class TaskController : Controller
    {

        private readonly ILogger<TaskController> _logger;
        private readonly TaskP _taskP;

        public TaskController(ILogger<TaskController> logger, TaskP taskP)
        {
            _logger = logger;
            _taskP = taskP;
        }

        [HttpGet]
        [Route("task")]
        public IActionResult Index()
        {
            _logger.LogInformation("La acción MyAction se ejecutó correctamente.");

            ResponseModel<string> result = new ResponseModel<string>();
            _taskP.getByTask("1");
            return StatusCode(404, new { result });
        }



        /*[HttpGet]
        [Route("price")]
        public async Task<ActionResult<string>> ProicessUser()
        {
            string api_key = Request.Headers.FirstOrDefault(x => x.Key == "api_key").Value;
            ResponseModel<List<FileCsvModel>> resultado;
            IPriceService service = new PriceService(_dbcontextMysql);
            resultado = await service.processValidationPermisos(api_key);
            return StatusCode(StatusCodes.Status200OK, new { resultado });
        }
        
                 [HttpPost]
        [Route("inventorystores")]
        public ActionResult<string> processInventoryStrores([FromHeader] HeaderModel headerData, [FromBody] BodyDataModel warehouse)
        {
            IInventoryServicesWarehouse inventoryServicesWarehouse = new InventoryServicesWarehouse(_dbcontextMysql, _dbpunoEr);
            ResponseModel<List<FileCsvModel>> resultado = inventoryServicesWarehouse.processValidationPermisosFilter(headerData.api_key, warehouse);
            return StatusCode(StatusCodes.Status200OK, new { resultado });
        }
         */
    }
}
