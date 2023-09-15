using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Services.TypeStateService.DeleteTypeStateService;
using Task.Services.TypeStateService.GetAllTypeStateService;
using Task.Services.TypeStateService.GetByTypeStateService;
using Task.Services.TypeStateService.PostSaveTypeStateService;
using Task.Services.TypeStateService.UpdateTypeStateService;

namespace Task.Controllers
{
    [Route("api/v1")]

    public class TypeStateController : Controller
    {
        private readonly IGetAllTypeStateService _iGetAllTypeStateService;
        private readonly IGetByTypeStateService _iGetByTypeStateService;
        private readonly IPostSaveTypeStateService _iPostSaveTypeStateService;
        private readonly IPutTypeStateService _iPutTypeStateService;
        private readonly IDeleteTypeStateService _iDeleteTypeStateService;

        public TypeStateController(IGetAllTypeStateService iGetAllTypeStateService, IGetByTypeStateService iGetByTypeStateService, IPostSaveTypeStateService iPostSaveTypeStateService, IPutTypeStateService iPutTypeStateService, IDeleteTypeStateService iDeleteTypeStateService)
        {
            _iGetAllTypeStateService = iGetAllTypeStateService;
            _iGetByTypeStateService = iGetByTypeStateService;
            _iPostSaveTypeStateService = iPostSaveTypeStateService;
            _iPutTypeStateService = iPutTypeStateService;
            _iDeleteTypeStateService = iDeleteTypeStateService;
        }

        [HttpGet]
        [Route("full-types-states")]
        public IActionResult GetAllTypeState()
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            respons = _iGetAllTypeStateService.GetAllTypeState();
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpGet]
        [Route("/by-type-state/{idTypeState}")]
        public IActionResult GetByTypeState([FromRoute] int idTypeState)
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            respons = _iGetByTypeStateService.GetByTypeState(idTypeState);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpPost]
        [Route("save-type-state")]
        public IActionResult SaveTypeState([FromBody] RequestTypeStateModel dataTypeState)
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            respons = _iPostSaveTypeStateService.SaveTypeState(dataTypeState);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpPut]
        [Route("update-type-state/{idTypeState}")]
        public IActionResult UpdateTypeState([FromRoute] int idTypeState, [FromBody] RequestTypeStateModel dataTypeState)
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            dataTypeState.TypeStateId = idTypeState;
            respons = _iPutTypeStateService.UpdateTypeState(dataTypeState);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpDelete]
        [Route("delete-type-state/{idTypeState}")]
        public IActionResult DeleteTypeState([FromRoute] int idTypeState)
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            respons = _iDeleteTypeStateService.DeleteTypeState(idTypeState);
            return StatusCode(respons.Status, new { respons.response });
        }
    }
}
