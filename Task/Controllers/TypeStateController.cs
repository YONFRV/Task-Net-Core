using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.TypeState;

namespace Task.Controllers
{
    [Route("api/v1")]

    public class TypeStateController : Controller
    {

        private readonly ITypeStateService _itypeState;

        public TypeStateController(ITypeStateService itypeState)
        {
            _itypeState = itypeState;
        }

        [HttpGet]
        [Route("full-types-states")]
        public IActionResult TypeState()
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            respons = _itypeState.FullTypeState();
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpGet]
        [Route("/by-type-state/{idTypeState}")]
        public IActionResult ByTypeState([FromRoute] int idTypeState)
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            respons = _itypeState.ByTypeState(idTypeState);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpPost]
        [Route("save-type-state")]
        public IActionResult SaveTypeState([FromBody] RequestTypeStateModel dataTypeState)
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            respons = _itypeState.SaveTypeState(dataTypeState);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpPut]
        [Route("update-type-state/{idTypeState}")]
        public IActionResult UpdateTypeState([FromRoute] int idTypeState, [FromBody] RequestTypeStateModel dataTypeState)
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            dataTypeState.TypeStateId = idTypeState;
            respons = _itypeState.UpdateTypeState(dataTypeState);
            return StatusCode(respons.Status, new { respons.response });
        }

        [HttpDelete]
        [Route("delete-type-state/{idTypeState}")]
        public IActionResult DeleteTypeState([FromRoute] int idTypeState)
        {
            ResponseGeneralModel<string> respons = new ResponseGeneralModel<string>();
            respons = _itypeState.DeleteTypeState(idTypeState);
            return StatusCode(respons.Status, new { respons.response });
        }
    }
}
