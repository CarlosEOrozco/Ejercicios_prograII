using EJMoneda.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EJMoneda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        //debo crear mi lista static, la inicializo con valores de monedas
        private static List<Moneda> monedas = new List<Moneda>
        {
            new Moneda { Nombre = "Dolar", Valor = 1250 },
            new Moneda { Nombre = "Peso Argentino", Valor = 1 }
        };

        [HttpGet] //Metodo GET que permite obtener todos los obj Moneda
        public IActionResult GetMoneda() //devuelve diferentes tipos de respuestas HTTP (por ejemplo, NotFound(), BadRequest()
        {
            return Ok(monedas); //devuelve una respuesta HTTP 200 OK con el contenido de monedas
        }

        // Ruta HTTP GET que acepta un parámetro 'nombre' en la URL
        [HttpGet("{nombre}")]
        public IActionResult GetMoneda(string nombre) 
        {
            // Busca la primera moneda en la lista 'monedas' cuyo nombre coincida (ignorando mayúsculas/minúsculas) con el parámetro 'nombre'
            var moneda = monedas.FirstOrDefault( m => m.Nombre.ToLower() == nombre.ToLower());

            // Si no se encuentra ninguna moneda con el nombre especificado, retorna un estado 404 (Not Found) con un mensaje
            if (moneda == null)
            {
                return NotFound("Moneda no encontrada");
            }

            // Si se encuentra la moneda, retorna un estado 200 (OK) con la información de la moneda
            return Ok(moneda);

        }

        // Define una ruta HTTP POST para crear una nueva moneda
        [HttpPost]
        public IActionResult PostMoneda(Moneda nuevaMoneda)
        {

            // Verifica si el objeto 'nuevaMoneda' es válido
            if (nuevaMoneda == null || string.IsNullOrEmpty(nuevaMoneda.Nombre))
            {
                return BadRequest("Datos de la moneda inválidos");
            }

            // Agrega la nueva moneda a la lista 'monedas'
            monedas.Add(nuevaMoneda);

            // Retorna un estado 201 (Created) con el objeto de la moneda creada y la URL para obtenerla
            return CreatedAtAction(nameof(GetMoneda), new { nombre = nuevaMoneda.Nombre }, nuevaMoneda);

            //CreatedAtAction(Este es el nombre del método al que se hace referencia para obtener el recurso recién creado,
            //Este es un objeto anónimo que contiene los parámetros necesarios para construir la URL del recurso recién creado,
            //Este es el objeto que se devuelve en la respuesta. Contiene los datos de la moneda recién creada.)
        }



    }
}
