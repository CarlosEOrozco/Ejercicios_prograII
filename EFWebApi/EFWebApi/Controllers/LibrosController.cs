using EFWebApi.Data.Repositories;
using EFWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        //agregar repository privado
        private ILibroRepository _repository;

        //ctor
        public LibrosController(ILibroRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<LibrosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception) 
            {
                return StatusCode(500, "Error");
            }

        }

        // GET api/<LibrosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LibrosController>
        [HttpPost]
        public IActionResult Post([FromBody] Libro value)
        {
            try
            {
                if (isValid(value)) //validacion de datos
                {
                    _repository.Create(value);
                    return Ok("Libro insertado");
                }
                else 
                {
                    return BadRequest("Los datos no son correctos o incompletos");
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }

        // PUT api/<LibrosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LibrosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok("Libro eliminado");
            }
            catch (Exception)
            { 
                return StatusCode(500, "Error");
            }

        }

        //hacer la valudacion
        private bool isValid(Libro value)
        {
            return !string.IsNullOrEmpty(value.Isbn) && !string.IsNullOrEmpty(value.Nombre) && !string.IsNullOrEmpty(value.FechaPublicacion) &&
        }
    }
}
