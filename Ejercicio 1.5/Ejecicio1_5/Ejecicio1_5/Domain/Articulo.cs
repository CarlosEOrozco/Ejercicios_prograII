using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Domain
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }  // Nombre del artículo
        public decimal PrecioUnitario { get; set; }  // Precio por unidad
    }
}
