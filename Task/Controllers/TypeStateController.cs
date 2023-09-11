using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using Task.Data;
using Task.Data.Entities;

namespace Task.Controllers
{
    [Route("api/v1")]

    public class TypeStateController : Controller
    {

        private readonly SeeriContext _context;
        public TypeStateController(SeeriContext seeriContext) {
            _context = seeriContext;
        }

        [HttpGet]
        [Route("full-types-states")]
        public IActionResult TypeState()
        {
            List<TypeStateEntity> listTypeState = _context.TypeStates.ToList();
            string result = JsonSerializer.Serialize(listTypeState);
            return StatusCode(200, new { result });
        }
    }
}
