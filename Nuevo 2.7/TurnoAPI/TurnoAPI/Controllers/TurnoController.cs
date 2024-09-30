using Microsoft.AspNetCore.Mvc;
using TurnoAPI.Models;
using TurnoAPI.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TurnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _service;
        public TurnoController(ITurnoService service)
        {
            _service = service;
        }

        // GET all
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var turno = _service.ObtenerTodos();
                return Ok(turno);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal server error");
            }

        }

        // GET api/<TurnoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST save
        [HttpPost]
        public IActionResult Post([FromBody] TTurno turno)
        {
            try
            {
                return Ok(_service.Guardar(turno));
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal server error");
            }
            
        }

        // PUT Update
        [HttpPut]
        public IActionResult Put([FromQuery]int id, [FromBody] TTurno turno)
        {

            return Ok(_service.Actualizar(turno,id));
        }

        // DELETE 
        [HttpDelete]
        public IActionResult Delete([FromQuery]int id)
        {
            return Ok(_service.Borrar(id));
        }
    }
}
