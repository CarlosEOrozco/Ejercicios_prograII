using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Domain
{
    public class DetalleFactura
    {
        public Articulo Articulo { get; set; }  // Relación con Artículo
        public int Cantidad { get; set; }  // Cantidad del artículo

                                           // Propiedad para referenciar la factura asociada
        public Factura Factura { get; set; }  // Esta línea es necesaria
    }
}
